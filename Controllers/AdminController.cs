using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UploadService _uploads;
    private readonly ILogger<AdminController> _logger;

    public AdminController(ApplicationDbContext db, UploadService uploads, ILogger<AdminController> logger)
    {
        _db = db;
        _uploads = uploads;
        _logger = logger;
    }

    // ============ Dashboard Home ============
    public async Task<IActionResult> Index()
    {
        ViewBag.ClientCount      = await _db.ClientItems.CountAsync();
        ViewBag.ServiceCount     = await _db.ServiceItems.CountAsync();
        ViewBag.PortfolioCount   = await _db.PortfolioItems.CountAsync();
        ViewBag.MediaCount       = await _db.MediaItems.CountAsync();
        ViewBag.TestimonialCount = await _db.Testimonials.CountAsync();
        ViewBag.FaqCount         = await _db.FaqItems.CountAsync();

        ViewBag.TotalLeads  = await _db.LeadRequests.CountAsync();
        ViewBag.NewLeads    = await _db.LeadRequests.CountAsync(l => l.Status == LeadStatus.New);
        ViewBag.UnreadLeads = await _db.LeadRequests.CountAsync(l => !l.IsRead);

        ViewBag.ContentLastUpdated = await _db.SiteContents
            .Select(s => s.UpdatedAt)
            .FirstOrDefaultAsync();

        ViewBag.LatestMedia = await _db.MediaItems
            .OrderByDescending(m => m.CreatedAt)
            .Take(6)
            .ToListAsync();

        ViewBag.LatestPortfolio = await _db.PortfolioItems
            .OrderByDescending(p => p.CreatedAt)
            .Take(4)
            .ToListAsync();

        ViewBag.LatestLeads = await _db.LeadRequests
            .OrderByDescending(l => l.CreatedAt)
            .Take(5)
            .ToListAsync();

        return View();
    }

    // ============ Settings (General + Hero + About + CTA banner) ============
    [HttpGet]
    public async Task<IActionResult> Settings()
    {
        var content = await _db.SiteContents.FirstOrDefaultAsync() ?? new SiteContent();
        return View(content);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Settings(
        SiteContent model,
        IFormFile? logoFile,
        IFormFile? logoDarkFile,
        IFormFile? faviconFile,
        IFormFile? heroImageFile,
        IFormFile? aboutImageFile,
        IFormFile? ctaBannerImageFile)
    {
        if (!ModelState.IsValid)
            return View(model);

        var content = await _db.SiteContents.FirstOrDefaultAsync();
        if (content is null)
        {
            content = new SiteContent();
            _db.SiteContents.Add(content);
        }

        // ---------- Copy text fields ----------
        content.Phone        = model.Phone;
        content.WhatsApp     = model.WhatsApp;
        content.Email        = model.Email;
        content.Address      = model.Address;
        content.FooterTextEn = model.FooterTextEn;
        content.FooterTextAr = model.FooterTextAr;
        content.LinkedInUrl  = model.LinkedInUrl;
        content.TwitterUrl   = model.TwitterUrl;
        content.GitHubUrl    = model.GitHubUrl;
        content.InstagramUrl = model.InstagramUrl;
        content.FacebookUrl  = model.FacebookUrl;

        content.HeroEyebrowEn          = model.HeroEyebrowEn;
        content.HeroEyebrowAr          = model.HeroEyebrowAr;
        content.HeroTitleEn            = model.HeroTitleEn;
        content.HeroTitleAr            = model.HeroTitleAr;
        content.HeroSubtitleEn         = model.HeroSubtitleEn;
        content.HeroSubtitleAr         = model.HeroSubtitleAr;
        content.HeroPrimaryCtaTextEn   = model.HeroPrimaryCtaTextEn;
        content.HeroPrimaryCtaTextAr   = model.HeroPrimaryCtaTextAr;
        content.HeroPrimaryCtaUrl      = model.HeroPrimaryCtaUrl;
        content.HeroSecondaryCtaTextEn = model.HeroSecondaryCtaTextEn;
        content.HeroSecondaryCtaTextAr = model.HeroSecondaryCtaTextAr;
        content.HeroSecondaryCtaUrl    = model.HeroSecondaryCtaUrl;

        content.AboutTitleEn = model.AboutTitleEn;
        content.AboutTitleAr = model.AboutTitleAr;
        content.AboutTextEn  = model.AboutTextEn;
        content.AboutTextAr  = model.AboutTextAr;

        content.ServicesTitleEn = model.ServicesTitleEn;
        content.ServicesTitleAr = model.ServicesTitleAr;
        content.ServicesLeadEn  = model.ServicesLeadEn;
        content.ServicesLeadAr  = model.ServicesLeadAr;

        content.PortfolioTitleEn = model.PortfolioTitleEn;
        content.PortfolioTitleAr = model.PortfolioTitleAr;
        content.PortfolioLeadEn  = model.PortfolioLeadEn;
        content.PortfolioLeadAr  = model.PortfolioLeadAr;

        content.CtaBannerTitleEn       = model.CtaBannerTitleEn;
        content.CtaBannerTitleAr       = model.CtaBannerTitleAr;
        content.CtaBannerSubtitleEn    = model.CtaBannerSubtitleEn;
        content.CtaBannerSubtitleAr    = model.CtaBannerSubtitleAr;
        content.CtaBannerButtonTextEn  = model.CtaBannerButtonTextEn;
        content.CtaBannerButtonTextAr  = model.CtaBannerButtonTextAr;
        content.CtaBannerButtonUrl     = model.CtaBannerButtonUrl;

        // ---------- Pre-selected media library URLs ----------
        if (!string.IsNullOrWhiteSpace(model.LogoUrl))          content.LogoUrl          = model.LogoUrl;
        if (!string.IsNullOrWhiteSpace(model.LogoDarkUrl))      content.LogoDarkUrl      = model.LogoDarkUrl;
        if (!string.IsNullOrWhiteSpace(model.FaviconUrl))       content.FaviconUrl       = model.FaviconUrl;
        if (!string.IsNullOrWhiteSpace(model.HeroImageUrl))     content.HeroImageUrl     = model.HeroImageUrl;
        if (!string.IsNullOrWhiteSpace(model.AboutImageUrl))    content.AboutImageUrl    = model.AboutImageUrl;
        if (!string.IsNullOrWhiteSpace(model.CtaBannerImageUrl))content.CtaBannerImageUrl= model.CtaBannerImageUrl;

        // ---------- File uploads (override pre-selected if a new file is sent) ----------
        await ApplyUpload(logoFile,           MediaCategories.Logos, u => content.LogoUrl           = u, content.LogoUrl);
        await ApplyUpload(logoDarkFile,       MediaCategories.Logos, u => content.LogoDarkUrl       = u, content.LogoDarkUrl);
        await ApplyUpload(faviconFile,        MediaCategories.Logos, u => content.FaviconUrl        = u, content.FaviconUrl);
        await ApplyUpload(heroImageFile,      MediaCategories.Hero,  u => content.HeroImageUrl      = u, content.HeroImageUrl);
        await ApplyUpload(aboutImageFile,     MediaCategories.About, u => content.AboutImageUrl     = u, content.AboutImageUrl);
        await ApplyUpload(ctaBannerImageFile, MediaCategories.Backgrounds, u => content.CtaBannerImageUrl = u, content.CtaBannerImageUrl);

        content.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        TempData["Success"] = "Settings saved successfully.";
        return RedirectToAction(nameof(Settings));
    }

    private async Task ApplyUpload(IFormFile? file, string category, Action<string> assign, string? currentValue)
    {
        if (file is null || file.Length == 0) return;

        if (!UploadService.IsValidUpload(file, out var error))
        {
            TempData["Error"] = error;
            return;
        }

        var url = await _uploads.SaveAndRegisterAsync(file, category);
        if (url == null) return;

        // Only delete the previous file if it was uploaded (under /uploads/), not a hand-typed external URL
        if (!string.IsNullOrWhiteSpace(currentValue) && currentValue.StartsWith("/uploads/", StringComparison.OrdinalIgnoreCase))
        {
            // Skip deletion: file is registered in Media Library and may be referenced elsewhere
        }

        assign(url);
    }

    // ============ Clients ============
    [HttpGet]
    public async Task<IActionResult> Clients()
    {
        var clients = await _db.ClientItems
            .OrderBy(c => c.DisplayOrder)
            .ToListAsync();
        return View(clients);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddClient(string name, string? websiteUrl, IFormFile? logoFile, string? logoUrl)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            TempData["Error"] = "Client name is required.";
            return RedirectToAction(nameof(Clients));
        }

        var finalLogoUrl = logoUrl;
        if (logoFile is { Length: > 0 })
        {
            if (!UploadService.IsValidUpload(logoFile, out var error))
            {
                TempData["Error"] = error;
                return RedirectToAction(nameof(Clients));
            }
            finalLogoUrl = await _uploads.SaveAndRegisterAsync(logoFile, MediaCategories.Clients);
        }

        if (string.IsNullOrWhiteSpace(finalLogoUrl))
        {
            TempData["Error"] = "A valid logo image is required (upload or pick from Media Library).";
            return RedirectToAction(nameof(Clients));
        }

        var maxOrder = await _db.ClientItems.MaxAsync(c => (int?)c.DisplayOrder) ?? 0;

        _db.ClientItems.Add(new ClientItem
        {
            Name         = name.Trim(),
            WebsiteUrl   = string.IsNullOrWhiteSpace(websiteUrl) ? null : websiteUrl.Trim(),
            LogoUrl      = finalLogoUrl,
            DisplayOrder = maxOrder + 1,
            CreatedAt    = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        TempData["Success"] = "Client added successfully.";
        return RedirectToAction(nameof(Clients));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _db.ClientItems.FindAsync(id);
        if (client is null)
        {
            TempData["Error"] = "Client not found.";
            return RedirectToAction(nameof(Clients));
        }

        _db.ClientItems.Remove(client);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Client deleted successfully.";
        return RedirectToAction(nameof(Clients));
    }
}
