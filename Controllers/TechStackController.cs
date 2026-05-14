using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class TechStackController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UploadService _uploads;

    public TechStackController(ApplicationDbContext db, UploadService uploads)
    {
        _db = db;
        _uploads = uploads;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await _db.TechStackItems.OrderBy(t => t.DisplayOrder).ToListAsync();
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(TechStackItem model, IFormFile? iconFile)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            TempData["Error"] = "Name is required.";
            return RedirectToAction(nameof(Index));
        }

        string? newIcon = null;
        if (iconFile is { Length: > 0 } && UploadService.IsValidUpload(iconFile, out _))
            newIcon = await _uploads.SaveAndRegisterAsync(iconFile, MediaCategories.Icons);

        if (model.Id == 0)
        {
            if (model.DisplayOrder == 0)
                model.DisplayOrder = (await _db.TechStackItems.MaxAsync(s => (int?)s.DisplayOrder) ?? 0) + 1;
            if (newIcon != null) model.IconUrl = newIcon;
            model.CreatedAt = DateTime.UtcNow;
            _db.TechStackItems.Add(model);
        }
        else
        {
            var item = await _db.TechStackItems.FindAsync(model.Id);
            if (item is null) return RedirectToAction(nameof(Index));

            item.Name         = model.Name;
            item.IconClass    = model.IconClass;
            item.DisplayOrder = model.DisplayOrder;
            item.IsActive     = model.IsActive;
            if (!string.IsNullOrWhiteSpace(model.IconUrl)) item.IconUrl = model.IconUrl;
            if (newIcon != null) item.IconUrl = newIcon;
        }

        await _db.SaveChangesAsync();
        TempData["Success"] = "Tech item saved.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.TechStackItems.FindAsync(id);
        if (item is not null)
        {
            _db.TechStackItems.Remove(item);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Tech item deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
