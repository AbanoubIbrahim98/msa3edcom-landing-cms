using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class TestimonialsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UploadService _uploads;

    public TestimonialsController(ApplicationDbContext db, UploadService uploads)
    {
        _db = db;
        _uploads = uploads;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await _db.Testimonials
            .OrderBy(t => t.DisplayOrder)
            .ToListAsync();
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(TestimonialItem model, IFormFile? avatarFile)
    {
        if (string.IsNullOrWhiteSpace(model.NameEn) || string.IsNullOrWhiteSpace(model.NameAr)
            || string.IsNullOrWhiteSpace(model.QuoteEn) || string.IsNullOrWhiteSpace(model.QuoteAr))
        {
            TempData["Error"] = "Name and quote (EN+AR) are required.";
            return RedirectToAction(nameof(Index));
        }

        string? newAvatar = null;
        if (avatarFile is { Length: > 0 } && UploadService.IsValidUpload(avatarFile, out _))
            newAvatar = await _uploads.SaveAndRegisterAsync(avatarFile, MediaCategories.Default);

        if (model.Id == 0)
        {
            if (model.DisplayOrder == 0)
                model.DisplayOrder = (await _db.Testimonials.MaxAsync(s => (int?)s.DisplayOrder) ?? 0) + 1;
            if (newAvatar != null) model.AvatarUrl = newAvatar;
            model.CreatedAt = DateTime.UtcNow;
            _db.Testimonials.Add(model);
        }
        else
        {
            var item = await _db.Testimonials.FindAsync(model.Id);
            if (item is null) return RedirectToAction(nameof(Index));

            item.NameEn       = model.NameEn;
            item.NameAr       = model.NameAr;
            item.RoleEn       = model.RoleEn;
            item.RoleAr       = model.RoleAr;
            item.CompanyEn    = model.CompanyEn;
            item.CompanyAr    = model.CompanyAr;
            item.QuoteEn      = model.QuoteEn;
            item.QuoteAr      = model.QuoteAr;
            item.Rating       = Math.Clamp(model.Rating, 1, 5);
            item.DisplayOrder = model.DisplayOrder;
            item.IsActive     = model.IsActive;
            if (!string.IsNullOrWhiteSpace(model.AvatarUrl)) item.AvatarUrl = model.AvatarUrl;
            if (newAvatar != null) item.AvatarUrl = newAvatar;
        }

        await _db.SaveChangesAsync();
        TempData["Success"] = "Testimonial saved.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Testimonials.FindAsync(id);
        if (item is not null)
        {
            _db.Testimonials.Remove(item);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Testimonial deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var item = await _db.Testimonials.FindAsync(id);
        if (item is not null)
        {
            item.IsActive = !item.IsActive;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
