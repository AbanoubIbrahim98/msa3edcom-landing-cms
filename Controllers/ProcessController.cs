using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class ProcessController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProcessController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await _db.ProcessSteps.OrderBy(s => s.DisplayOrder).ToListAsync();
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(ProcessStep model)
    {
        if (string.IsNullOrWhiteSpace(model.TitleEn) || string.IsNullOrWhiteSpace(model.TitleAr))
        {
            TempData["Error"] = "Title (EN+AR) is required.";
            return RedirectToAction(nameof(Index));
        }

        if (model.Id == 0)
        {
            if (model.DisplayOrder == 0)
                model.DisplayOrder = (await _db.ProcessSteps.MaxAsync(s => (int?)s.DisplayOrder) ?? 0) + 1;
            model.CreatedAt = DateTime.UtcNow;
            _db.ProcessSteps.Add(model);
        }
        else
        {
            var item = await _db.ProcessSteps.FindAsync(model.Id);
            if (item is null) return RedirectToAction(nameof(Index));

            item.TitleEn       = model.TitleEn;
            item.TitleAr       = model.TitleAr;
            item.DescriptionEn = model.DescriptionEn;
            item.DescriptionAr = model.DescriptionAr;
            item.IconClass     = model.IconClass;
            item.DisplayOrder  = model.DisplayOrder;
            item.IsActive      = model.IsActive;
        }

        await _db.SaveChangesAsync();
        TempData["Success"] = "Process step saved.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.ProcessSteps.FindAsync(id);
        if (item is not null)
        {
            _db.ProcessSteps.Remove(item);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Process step deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
