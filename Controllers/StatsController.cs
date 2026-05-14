using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class StatsController : Controller
{
    private readonly ApplicationDbContext _db;

    public StatsController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await _db.StatItems.OrderBy(s => s.DisplayOrder).ToListAsync();
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(StatItem model)
    {
        if (string.IsNullOrWhiteSpace(model.LabelEn) || string.IsNullOrWhiteSpace(model.LabelAr)
            || string.IsNullOrWhiteSpace(model.Value))
        {
            TempData["Error"] = "Label (EN+AR) and value are required.";
            return RedirectToAction(nameof(Index));
        }

        if (model.Id == 0)
        {
            if (model.DisplayOrder == 0)
                model.DisplayOrder = (await _db.StatItems.MaxAsync(s => (int?)s.DisplayOrder) ?? 0) + 1;
            model.CreatedAt = DateTime.UtcNow;
            _db.StatItems.Add(model);
        }
        else
        {
            var item = await _db.StatItems.FindAsync(model.Id);
            if (item is null) return RedirectToAction(nameof(Index));

            item.LabelEn      = model.LabelEn;
            item.LabelAr      = model.LabelAr;
            item.Value        = model.Value;
            item.Suffix       = model.Suffix;
            item.IconClass    = model.IconClass;
            item.DisplayOrder = model.DisplayOrder;
            item.IsActive     = model.IsActive;
        }

        await _db.SaveChangesAsync();
        TempData["Success"] = "Stat saved.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.StatItems.FindAsync(id);
        if (item is not null)
        {
            _db.StatItems.Remove(item);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Stat deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
