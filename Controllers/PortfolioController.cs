using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class PortfolioController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UploadService _uploads;

    public PortfolioController(ApplicationDbContext db, UploadService uploads)
    {
        _db = db;
        _uploads = uploads;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await _db.PortfolioItems
            .OrderBy(p => p.DisplayOrder)
            .ToListAsync();
        return View(items);
    }

    [HttpGet]
    public IActionResult Create() => View(new PortfolioItem { IsActive = true });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PortfolioItem model, IFormFile? thumbnailFile)
    {
        if (!ModelState.IsValid) return View(model);

        if (thumbnailFile is { Length: > 0 } && UploadService.IsValidUpload(thumbnailFile, out _))
            model.ThumbnailUrl = await _uploads.SaveAndRegisterAsync(thumbnailFile, MediaCategories.Portfolio);

        if (model.DisplayOrder == 0)
            model.DisplayOrder = (await _db.PortfolioItems.MaxAsync(p => (int?)p.DisplayOrder) ?? 0) + 1;

        model.CreatedAt = DateTime.UtcNow;
        _db.PortfolioItems.Add(model);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Project added.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.PortfolioItems.FindAsync(id);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PortfolioItem model, IFormFile? thumbnailFile)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        var item = await _db.PortfolioItems.FindAsync(id);
        if (item is null) return NotFound();

        item.TitleEn       = model.TitleEn;
        item.TitleAr       = model.TitleAr;
        item.DescriptionEn = model.DescriptionEn;
        item.DescriptionAr = model.DescriptionAr;
        item.Technologies  = model.Technologies;
        item.ProjectUrl    = model.ProjectUrl;
        item.ThumbnailUrl  = model.ThumbnailUrl;
        item.DisplayOrder  = model.DisplayOrder;
        item.IsFeatured    = model.IsFeatured;
        item.IsActive      = model.IsActive;

        if (thumbnailFile is { Length: > 0 } && UploadService.IsValidUpload(thumbnailFile, out _))
            item.ThumbnailUrl = await _uploads.SaveAndRegisterAsync(thumbnailFile, MediaCategories.Portfolio);

        await _db.SaveChangesAsync();
        TempData["Success"] = "Project updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.PortfolioItems.FindAsync(id);
        if (item is not null)
        {
            _db.PortfolioItems.Remove(item);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Project deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var item = await _db.PortfolioItems.FindAsync(id);
        if (item is not null)
        {
            item.IsActive = !item.IsActive;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleFeatured(int id)
    {
        var item = await _db.PortfolioItems.FindAsync(id);
        if (item is not null)
        {
            item.IsFeatured = !item.IsFeatured;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder(int id, int direction)
    {
        var ordered = await _db.PortfolioItems.OrderBy(p => p.DisplayOrder).ToListAsync();
        var idx = ordered.FindIndex(s => s.Id == id);
        if (idx < 0) return RedirectToAction(nameof(Index));

        var swapWith = direction < 0 ? idx - 1 : idx + 1;
        if (swapWith < 0 || swapWith >= ordered.Count) return RedirectToAction(nameof(Index));

        (ordered[idx].DisplayOrder, ordered[swapWith].DisplayOrder) =
            (ordered[swapWith].DisplayOrder, ordered[idx].DisplayOrder);

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
