using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class FaqController : Controller
{
    private readonly ApplicationDbContext _db;

    public FaqController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await _db.FaqItems.OrderBy(f => f.DisplayOrder).ToListAsync();
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(FaqItem model)
    {
        if (string.IsNullOrWhiteSpace(model.QuestionEn) || string.IsNullOrWhiteSpace(model.AnswerEn)
            || string.IsNullOrWhiteSpace(model.QuestionAr) || string.IsNullOrWhiteSpace(model.AnswerAr))
        {
            TempData["Error"] = "Question and answer (EN+AR) are required.";
            return RedirectToAction(nameof(Index));
        }

        if (model.Id == 0)
        {
            if (model.DisplayOrder == 0)
                model.DisplayOrder = (await _db.FaqItems.MaxAsync(s => (int?)s.DisplayOrder) ?? 0) + 1;
            model.CreatedAt = DateTime.UtcNow;
            _db.FaqItems.Add(model);
        }
        else
        {
            var item = await _db.FaqItems.FindAsync(model.Id);
            if (item is null) return RedirectToAction(nameof(Index));
            item.QuestionEn   = model.QuestionEn;
            item.QuestionAr   = model.QuestionAr;
            item.AnswerEn     = model.AnswerEn;
            item.AnswerAr     = model.AnswerAr;
            item.DisplayOrder = model.DisplayOrder;
            item.IsActive     = model.IsActive;
        }

        await _db.SaveChangesAsync();
        TempData["Success"] = "FAQ saved.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.FaqItems.FindAsync(id);
        if (item is not null)
        {
            _db.FaqItems.Remove(item);
            await _db.SaveChangesAsync();
            TempData["Success"] = "FAQ deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var item = await _db.FaqItems.FindAsync(id);
        if (item is not null)
        {
            item.IsActive = !item.IsActive;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
