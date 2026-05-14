using Microsoft.EntityFrameworkCore;

namespace Msa3edcomAdmin.Models;

/// <summary>
/// Single source of truth for file uploads, validation, deletion, and Media Library
/// registration. Used by every admin controller so the rules stay consistent.
/// </summary>
public class UploadService
{
    private static readonly string[] AllowedExtensions =
        { ".jpg", ".jpeg", ".png", ".webp", ".svg" };

    public const long MaxFileSize = 4 * 1024 * 1024; // 4 MB

    private readonly IWebHostEnvironment _env;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<UploadService> _logger;

    public UploadService(IWebHostEnvironment env, ApplicationDbContext db, ILogger<UploadService> logger)
    {
        _env = env;
        _db = db;
        _logger = logger;
    }

    public static string AllowedExtensionsText =>
        string.Join(", ", AllowedExtensions);

    /// <summary>
    /// Saves the uploaded file under wwwroot/uploads/media/{category}/ and records it
    /// in the Media Library. Returns the public URL or null on failure.
    /// </summary>
    public async Task<string?> SaveAndRegisterAsync(
        IFormFile? file,
        string category,
        string? altText = null)
    {
        var saved = await SaveRawAsync(file, category);
        if (saved is null) return null;

        try
        {
            _db.MediaItems.Add(new MediaItem
            {
                FileName  = Path.GetFileName(saved.RelativeUrl),
                FileUrl   = saved.RelativeUrl,
                FileType  = saved.Extension.TrimStart('.'),
                Category  = NormalizeCategory(category),
                AltText   = altText,
                SizeBytes = saved.Size,
                CreatedAt = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to register media item for {Url}", saved.RelativeUrl);
        }

        return saved.RelativeUrl;
    }

    /// <summary>
    /// Lower-level save that does NOT register in the Media Library. Used by the
    /// Media Library upload endpoint itself (which adds its own DB row).
    /// </summary>
    public async Task<UploadResult?> SaveRawAsync(IFormFile? file, string category)
    {
        if (file is null || file.Length == 0) return null;

        if (file.Length > MaxFileSize)
        {
            _logger.LogInformation("Rejected upload: file too large ({Size} bytes)", file.Length);
            return null;
        }

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(ext))
        {
            _logger.LogInformation("Rejected upload: extension {Ext} not allowed", ext);
            return null;
        }

        var folder = NormalizeCategory(category);
        var uploadsRoot   = Path.Combine(_env.WebRootPath, "uploads");
        var uploadsFolder = Path.Combine(uploadsRoot, "media", folder);
        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid():N}{ext}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        // Path traversal guard
        var resolvedFile = Path.GetFullPath(filePath);
        var resolvedRoot = Path.GetFullPath(uploadsRoot);
        if (!resolvedFile.StartsWith(resolvedRoot, StringComparison.OrdinalIgnoreCase))
            return null;

        try
        {
            await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            await file.CopyToAsync(stream);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "File upload failed for {File}", file.FileName);
            return null;
        }

        return new UploadResult(
            RelativeUrl: $"/uploads/media/{folder}/{fileName}",
            Extension: ext,
            Size: file.Length);
    }

    /// <summary>
    /// Removes a physical file from disk if it lives under wwwroot/uploads.
    /// Safe to call with null/empty values.
    /// </summary>
    public void DeleteFile(string? relativeUrl)
    {
        if (string.IsNullOrWhiteSpace(relativeUrl)) return;
        if (!relativeUrl.StartsWith("/uploads/", StringComparison.OrdinalIgnoreCase)) return;

        try
        {
            var trimmed = relativeUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            var path    = Path.Combine(_env.WebRootPath, trimmed);

            var resolved    = Path.GetFullPath(path);
            var uploadsRoot = Path.GetFullPath(Path.Combine(_env.WebRootPath, "uploads"));

            if (!resolved.StartsWith(uploadsRoot, StringComparison.OrdinalIgnoreCase))
                return;

            if (File.Exists(resolved))
                File.Delete(resolved);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not delete upload {Url}", relativeUrl);
        }
    }

    public static bool IsValidUpload(IFormFile file, out string? error)
    {
        error = null;

        if (file.Length == 0)
        {
            error = "Empty file.";
            return false;
        }
        if (file.Length > MaxFileSize)
        {
            error = $"File exceeds maximum size of {MaxFileSize / 1024 / 1024} MB.";
            return false;
        }
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(ext))
        {
            error = $"File type not allowed. Allowed: {string.Join(", ", AllowedExtensions)}";
            return false;
        }
        return true;
    }

    // ===========================================================
    // Lead attachments — separate allow-list (PDF/DOCX/PNG/JPG),
    // stored under wwwroot/uploads/leads/, NOT in the Media Library.
    // ===========================================================
    private static readonly string[] LeadAttachmentExtensions =
        { ".pdf", ".doc", ".docx", ".png", ".jpg", ".jpeg" };
    private const long LeadMaxFileSize = 8 * 1024 * 1024; // 8 MB

    public static string LeadAttachmentExtensionsText =>
        string.Join(", ", LeadAttachmentExtensions);

    public static bool IsValidLeadAttachment(IFormFile file, out string? error)
    {
        error = null;
        if (file.Length == 0) { error = "Empty file."; return false; }
        if (file.Length > LeadMaxFileSize)
        {
            error = $"File exceeds maximum size of {LeadMaxFileSize / 1024 / 1024} MB.";
            return false;
        }
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!LeadAttachmentExtensions.Contains(ext))
        {
            error = $"File type not allowed. Allowed: {string.Join(", ", LeadAttachmentExtensions)}";
            return false;
        }
        return true;
    }

    public async Task<LeadAttachment?> SaveLeadAttachmentAsync(IFormFile? file)
    {
        if (file is null || file.Length == 0) return null;
        if (!IsValidLeadAttachment(file, out _)) return null;

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var uploadsRoot   = Path.Combine(_env.WebRootPath, "uploads");
        var uploadsFolder = Path.Combine(uploadsRoot, "leads");
        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid():N}{ext}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        var resolvedFile = Path.GetFullPath(filePath);
        var resolvedRoot = Path.GetFullPath(uploadsRoot);
        if (!resolvedFile.StartsWith(resolvedRoot, StringComparison.OrdinalIgnoreCase))
            return null;

        try
        {
            await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            await file.CopyToAsync(stream);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lead attachment upload failed for {File}", file.FileName);
            return null;
        }

        var safeOriginal = Path.GetFileName(file.FileName);
        return new LeadAttachment(
            RelativeUrl: $"/uploads/leads/{fileName}",
            OriginalName: safeOriginal.Length > 200 ? safeOriginal[..200] : safeOriginal);
    }

    public record LeadAttachment(string RelativeUrl, string OriginalName);

    private static string NormalizeCategory(string? category)
    {
        if (string.IsNullOrWhiteSpace(category)) return MediaCategories.Default;
        var c = category.Trim().ToLowerInvariant();
        return MediaCategories.All.Contains(c) ? c : MediaCategories.Default;
    }

    public record UploadResult(string RelativeUrl, string Extension, long Size);
}
