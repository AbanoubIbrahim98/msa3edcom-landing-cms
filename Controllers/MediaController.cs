using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class MediaController : Controller
{
    private const int PageSize = 24;

    private readonly ApplicationDbContext _db;
    private readonly UploadService _uploads;
    private readonly ILogger<MediaController> _logger;

    public MediaController(ApplicationDbContext db, UploadService uploads, ILogger<MediaController> logger)
    {
        _db = db;
        _uploads = uploads;
        _logger = logger;
    }

    // ============ Library page ============
    [HttpGet]
    public async Task<IActionResult> Index(string? category, string? q, int page = 1)
    {
        var query = _db.MediaItems.AsQueryable();

        if (!string.IsNullOrWhiteSpace(category) && MediaCategories.All.Contains(category.ToLower()))
            query = query.Where(m => m.Category == category);

        if (!string.IsNullOrWhiteSpace(q))
        {
            var like = $"%{q.Trim()}%";
            query = query.Where(m =>
                EF.Functions.Like(m.FileName, like) ||
                (m.AltText != null && EF.Functions.Like(m.AltText, like)));
        }

        var total = await query.CountAsync();
        page = Math.Max(1, page);
        var totalPages = (int)Math.Ceiling(total / (double)PageSize);

        var items = await query
            .OrderByDescending(m => m.CreatedAt)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        ViewBag.Category    = category;
        ViewBag.Query       = q;
        ViewBag.Page        = page;
        ViewBag.TotalPages  = Math.Max(1, totalPages);
        ViewBag.Total       = total;
        ViewBag.Categories  = MediaCategories.All;

        return View(items);
    }

    // ============ Upload (multipart, can be ajax or normal POST) ============
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestSizeLimit(20 * 1024 * 1024)]
    public async Task<IActionResult> Upload(string? category, string? altText, List<IFormFile> files)
    {
        var saved = 0;
        var errors = new List<string>();

        foreach (var file in files ?? new List<IFormFile>())
        {
            if (!UploadService.IsValidUpload(file, out var error))
            {
                errors.Add($"{file.FileName}: {error}");
                continue;
            }

            var url = await _uploads.SaveAndRegisterAsync(file, category ?? MediaCategories.Default, altText);
            if (url != null) saved++;
            else errors.Add($"{file.FileName}: could not save.");
        }

        if (saved > 0) TempData["Success"] = $"Uploaded {saved} file(s).";
        if (errors.Count > 0) TempData["Error"] = string.Join(" · ", errors.Take(3));

        return RedirectToAction(nameof(Index), new { category });
    }

    // ============ Delete ============
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, string? category)
    {
        var item = await _db.MediaItems.FindAsync(id);
        if (item is null)
        {
            TempData["Error"] = "File not found.";
            return RedirectToAction(nameof(Index), new { category });
        }

        _uploads.DeleteFile(item.FileUrl);
        _db.MediaItems.Remove(item);
        await _db.SaveChangesAsync();

        TempData["Success"] = "File deleted.";
        return RedirectToAction(nameof(Index), new { category });
    }

    // ============ JSON list — for the media picker modal across the admin ============
    [HttpGet]
    public async Task<IActionResult> List(string? category, string? q, int page = 1)
    {
        var query = _db.MediaItems.AsQueryable();
        if (!string.IsNullOrWhiteSpace(category) && MediaCategories.All.Contains(category.ToLower()))
            query = query.Where(m => m.Category == category);
        if (!string.IsNullOrWhiteSpace(q))
        {
            var like = $"%{q.Trim()}%";
            query = query.Where(m =>
                EF.Functions.Like(m.FileName, like) ||
                (m.AltText != null && EF.Functions.Like(m.AltText, like)));
        }

        var pageSize = 30;
        page = Math.Max(1, page);
        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(m => m.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(m => new
            {
                id = m.Id,
                url = m.FileUrl,
                name = m.FileName,
                type = m.FileType,
                category = m.Category,
                alt = m.AltText
            })
            .ToListAsync();

        return Json(new
        {
            items,
            total,
            page,
            pageSize,
            totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize))
        });
    }
}
