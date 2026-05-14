using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.Controllers;

[Authorize]
public class LeadsController : Controller
{
    private const int PageSize = 20;

    private readonly ApplicationDbContext _db;
    private readonly UploadService _uploads;
    private readonly ILogger<LeadsController> _logger;

    public LeadsController(ApplicationDbContext db, UploadService uploads, ILogger<LeadsController> logger)
    {
        _db = db;
        _uploads = uploads;
        _logger = logger;
    }

    // ============ List ============
    [HttpGet]
    public async Task<IActionResult> Index(string? status, string? q, int page = 1)
    {
        var query = _db.LeadRequests.AsQueryable();

        if (!string.IsNullOrWhiteSpace(status) && LeadStatus.All.Contains(status))
            query = query.Where(l => l.Status == status);

        if (!string.IsNullOrWhiteSpace(q))
        {
            var like = $"%{q.Trim()}%";
            query = query.Where(l =>
                EF.Functions.Like(l.FullName, like) ||
                EF.Functions.Like(l.Email, like) ||
                (l.CompanyName != null && EF.Functions.Like(l.CompanyName, like)) ||
                (l.PhoneNumber != null && EF.Functions.Like(l.PhoneNumber, like)) ||
                EF.Functions.Like(l.Message, like));
        }

        var total = await query.CountAsync();
        page = Math.Max(1, page);
        var totalPages = (int)Math.Ceiling(total / (double)PageSize);

        var items = await query
            .OrderByDescending(l => l.CreatedAt)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        ViewBag.Status        = status;
        ViewBag.Query         = q;
        ViewBag.Page          = page;
        ViewBag.TotalPages    = Math.Max(1, totalPages);
        ViewBag.Total         = total;
        ViewBag.StatusOptions = LeadStatus.All;

        ViewBag.TotalLeads    = await _db.LeadRequests.CountAsync();
        ViewBag.NewLeads      = await _db.LeadRequests.CountAsync(l => l.Status == LeadStatus.New);
        ViewBag.UnreadLeads   = await _db.LeadRequests.CountAsync(l => !l.IsRead);
        ViewBag.ClosedLeads   = await _db.LeadRequests.CountAsync(l => l.Status == LeadStatus.Closed);

        return View(items);
    }

    // ============ Details (JSON for modal) ============
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var lead = await _db.LeadRequests.FindAsync(id);
        if (lead is null) return NotFound();

        if (!lead.IsRead)
        {
            lead.IsRead = true;
            lead.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

        return Json(new
        {
            id             = lead.Id,
            fullName       = lead.FullName,
            email          = lead.Email,
            phoneNumber    = lead.PhoneNumber,
            whatsAppNumber = lead.WhatsAppNumber,
            companyName    = lead.CompanyName,
            serviceType    = lead.ServiceType,
            budget         = lead.Budget,
            message        = lead.Message,
            attachmentUrl  = lead.AttachmentUrl,
            attachmentName = lead.AttachmentName,
            status         = lead.Status,
            isRead         = lead.IsRead,
            createdAt      = lead.CreatedAt.ToString("MMM dd, yyyy HH:mm")
        });
    }

    // ============ Change status ============
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(int id, string status, string? returnUrl)
    {
        if (!LeadStatus.All.Contains(status))
        {
            TempData["Error"] = "Invalid status.";
            return RedirectToActionOrReturn(returnUrl);
        }

        var lead = await _db.LeadRequests.FindAsync(id);
        if (lead is null)
        {
            TempData["Error"] = "Lead not found.";
            return RedirectToActionOrReturn(returnUrl);
        }

        lead.Status = status;
        lead.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        TempData["Success"] = $"Status updated to {status}.";
        return RedirectToActionOrReturn(returnUrl);
    }

    // ============ Toggle read ============
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleRead(int id, string? returnUrl)
    {
        var lead = await _db.LeadRequests.FindAsync(id);
        if (lead is null)
        {
            TempData["Error"] = "Lead not found.";
            return RedirectToActionOrReturn(returnUrl);
        }

        lead.IsRead = !lead.IsRead;
        lead.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return RedirectToActionOrReturn(returnUrl);
    }

    // ============ Delete ============
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, string? returnUrl)
    {
        var lead = await _db.LeadRequests.FindAsync(id);
        if (lead is null)
        {
            TempData["Error"] = "Lead not found.";
            return RedirectToActionOrReturn(returnUrl);
        }

        _uploads.DeleteFile(lead.AttachmentUrl);
        _db.LeadRequests.Remove(lead);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Lead deleted.";
        return RedirectToActionOrReturn(returnUrl);
    }

    private IActionResult RedirectToActionOrReturn(string? returnUrl)
    {
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);
        return RedirectToAction(nameof(Index));
    }
}
