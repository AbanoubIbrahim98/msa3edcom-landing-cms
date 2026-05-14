using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class SiteSettingsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UploadService _uploads;

    public SiteSettingsController(ApplicationDbContext db, UploadService uploads)
    {
        _db = db;
        _uploads = uploads;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var s = await _db.SiteSettings.FirstOrDefaultAsync() ?? new SiteSettings();
        return View(s);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(SiteSettings model, IFormFile? ogImageFile, IFormFile? faviconFile)
    {
        if (!ModelState.IsValid) return View(model);

        var settings = await _db.SiteSettings.FirstOrDefaultAsync();
        if (settings is null)
        {
            settings = new SiteSettings();
            _db.SiteSettings.Add(settings);
        }

        settings.MetaTitleEn          = model.MetaTitleEn;
        settings.MetaTitleAr          = model.MetaTitleAr;
        settings.MetaDescriptionEn    = model.MetaDescriptionEn;
        settings.MetaDescriptionAr    = model.MetaDescriptionAr;
        settings.MetaKeywordsEn       = model.MetaKeywordsEn;
        settings.MetaKeywordsAr       = model.MetaKeywordsAr;
        settings.GoogleAnalyticsCode  = model.GoogleAnalyticsCode;
        settings.PrimaryColor         = model.PrimaryColor;
        settings.SecondaryColor       = model.SecondaryColor;

        if (!string.IsNullOrWhiteSpace(model.OpenGraphImage)) settings.OpenGraphImage = model.OpenGraphImage;
        if (!string.IsNullOrWhiteSpace(model.FaviconUrl))     settings.FaviconUrl     = model.FaviconUrl;

        if (ogImageFile is { Length: > 0 } && UploadService.IsValidUpload(ogImageFile, out _))
            settings.OpenGraphImage = await _uploads.SaveAndRegisterAsync(ogImageFile, MediaCategories.Backgrounds);

        if (faviconFile is { Length: > 0 } && UploadService.IsValidUpload(faviconFile, out _))
            settings.FaviconUrl = await _uploads.SaveAndRegisterAsync(faviconFile, MediaCategories.Logos);

        settings.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        TempData["Success"] = "Site settings saved.";
        return RedirectToAction(nameof(Index));
    }
}
