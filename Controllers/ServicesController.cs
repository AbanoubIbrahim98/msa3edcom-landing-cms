using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class ServicesController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UploadService _uploads;

    public ServicesController(ApplicationDbContext db, UploadService uploads)
    {
        _db = db;
        _uploads = uploads;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await _db.ServiceItems
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();
        return View(items);
    }

    [HttpGet]
    public IActionResult Create() => View(new ServiceItem { IsActive = true });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceItem model, IFormFile? iconFile, IFormFile? imageFile)
    {
        if (!ModelState.IsValid) return View(model);

        if (iconFile is { Length: > 0 } && UploadService.IsValidUpload(iconFile, out _))
            model.IconUrl = await _uploads.SaveAndRegisterAsync(iconFile, MediaCategories.Icons);

        if (imageFile is { Length: > 0 } && UploadService.IsValidUpload(imageFile, out _))
            model.ImageUrl = await _uploads.SaveAndRegisterAsync(imageFile, MediaCategories.Services);

        if (model.DisplayOrder == 0)
            model.DisplayOrder = (await _db.ServiceItems.MaxAsync(s => (int?)s.DisplayOrder) ?? 0) + 1;

        model.CreatedAt = DateTime.UtcNow;
        _db.ServiceItems.Add(model);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Service added.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.ServiceItems.FindAsync(id);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ServiceItem model, IFormFile? iconFile, IFormFile? imageFile)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        var item = await _db.ServiceItems.FindAsync(id);
        if (item is null) return NotFound();

        item.TitleEn       = model.TitleEn;
        item.TitleAr       = model.TitleAr;
        item.DescriptionEn = model.DescriptionEn;
        item.DescriptionAr = model.DescriptionAr;
        item.IconClass     = model.IconClass;
        item.IconUrl       = model.IconUrl;
        item.ImageUrl      = model.ImageUrl;
        item.DisplayOrder  = model.DisplayOrder;
        item.IsActive      = model.IsActive;

        if (iconFile is { Length: > 0 } && UploadService.IsValidUpload(iconFile, out _))
            item.IconUrl = await _uploads.SaveAndRegisterAsync(iconFile, MediaCategories.Icons);

        if (imageFile is { Length: > 0 } && UploadService.IsValidUpload(imageFile, out _))
            item.ImageUrl = await _uploads.SaveAndRegisterAsync(imageFile, MediaCategories.Services);

        await _db.SaveChangesAsync();
        TempData["Success"] = "Service updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.ServiceItems.FindAsync(id);
        if (item is not null)
        {
            _db.ServiceItems.Remove(item);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Service deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var item = await _db.ServiceItems.FindAsync(id);
        if (item is not null)
        {
            item.IsActive = !item.IsActive;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder(int id, int direction)
    {
        var item = await _db.ServiceItems.FindAsync(id);
        if (item is null) return RedirectToAction(nameof(Index));

        var ordered = await _db.ServiceItems.OrderBy(s => s.DisplayOrder).ToListAsync();
        var idx = ordered.FindIndex(s => s.Id == id);
        var swapWith = direction < 0 ? idx - 1 : idx + 1;
        if (swapWith < 0 || swapWith >= ordered.Count) return RedirectToAction(nameof(Index));

        (ordered[idx].DisplayOrder, ordered[swapWith].DisplayOrder) =
            (ordered[swapWith].DisplayOrder, ordered[idx].DisplayOrder);

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
