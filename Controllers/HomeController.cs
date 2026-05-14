using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Msa3edcomAdmin.Models;
using Msa3edcomAdmin.ViewModels;

namespace Msa3edcomAdmin.Controllers;

public class HomeController : Controller
{
    private const string LangCookieName = ".Msa3edcom.Lang";
    private const string DefaultLanguage = "ar";

    private readonly ApplicationDbContext _db;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext db, ILogger<HomeController> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string? lang)
    {
        var language = ResolveLanguage(lang);

        try
        {
            var content  = await _db.SiteContents.AsNoTracking().FirstOrDefaultAsync() ?? new SiteContent();
            var settings = await _db.SiteSettings.AsNoTracking().FirstOrDefaultAsync() ?? new SiteSettings();

            var clients      = await _db.ClientItems.AsNoTracking().OrderBy(c => c.DisplayOrder).ToListAsync();
            var services     = await _db.ServiceItems.AsNoTracking().Where(s => s.IsActive).OrderBy(s => s.DisplayOrder).ToListAsync();
            var portfolio    = await _db.PortfolioItems.AsNoTracking().Where(p => p.IsActive).OrderBy(p => p.DisplayOrder).ToListAsync();
            var testimonials = await _db.Testimonials.AsNoTracking().Where(t => t.IsActive).OrderBy(t => t.DisplayOrder).ToListAsync();
            var faqs         = await _db.FaqItems.AsNoTracking().Where(f => f.IsActive).OrderBy(f => f.DisplayOrder).ToListAsync();
            var techStack    = await _db.TechStackItems.AsNoTracking().Where(t => t.IsActive).OrderBy(t => t.DisplayOrder).ToListAsync();
            var stats        = await _db.StatItems.AsNoTracking().Where(s => s.IsActive).OrderBy(s => s.DisplayOrder).ToListAsync();
            var process      = await _db.ProcessSteps.AsNoTracking().Where(p => p.IsActive).OrderBy(p => p.DisplayOrder).ToListAsync();

            var vm = new LandingPageViewModel
            {
                Content      = content,
                Settings     = settings,
                Clients      = clients,
                Services     = services,
                Portfolio    = portfolio,
                Testimonials = testimonials,
                Faqs         = faqs,
                TechStack    = techStack,
                Stats        = stats,
                Process      = process,
                Language     = language
            };

            return View(vm);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load landing page content.");
            return View(new LandingPageViewModel { Language = language });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetLanguage(string language, string? returnUrl)
    {
        var lang = (language == "en") ? "en" : "ar";
        Response.Cookies.Append(LangCookieName, lang, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(1),
            HttpOnly = false,
            IsEssential = true,
            SameSite = SameSiteMode.Lax
        });

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }

    // ============ Public lead submission (AJAX or fallback POST) ============
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestSizeLimit(12 * 1024 * 1024)]
    public async Task<IActionResult> SubmitLead(
        LeadRequest model,
        IFormFile? attachment,
        [FromServices] UploadService uploads,
        [FromServices] EmailService email)
    {
        var lang = ResolveLanguage(null);
        var isRtl = lang == "ar";

        bool isAjax = string.Equals(Request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);

        if (string.IsNullOrWhiteSpace(model.FullName) ||
            string.IsNullOrWhiteSpace(model.Email)    ||
            string.IsNullOrWhiteSpace(model.Message))
        {
            var msg = isRtl ? "يرجى تعبئة الاسم والبريد والرسالة." : "Please fill in name, email, and message.";
            return RespondError(isAjax, msg);
        }

        if (!new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(model.Email))
        {
            var msg = isRtl ? "البريد الإلكتروني غير صالح." : "Email address is not valid.";
            return RespondError(isAjax, msg);
        }

        var lead = new LeadRequest
        {
            FullName       = model.FullName.Trim(),
            Email          = model.Email.Trim(),
            PhoneNumber    = string.IsNullOrWhiteSpace(model.PhoneNumber)    ? null : model.PhoneNumber.Trim(),
            WhatsAppNumber = string.IsNullOrWhiteSpace(model.WhatsAppNumber) ? null : model.WhatsAppNumber.Trim(),
            CompanyName    = string.IsNullOrWhiteSpace(model.CompanyName)    ? null : model.CompanyName.Trim(),
            ServiceType    = string.IsNullOrWhiteSpace(model.ServiceType)    ? null : model.ServiceType.Trim(),
            Budget         = string.IsNullOrWhiteSpace(model.Budget)         ? null : model.Budget.Trim(),
            Message        = model.Message.Trim(),
            Status         = LeadStatus.New,
            IsRead         = false,
            CreatedAt      = DateTime.UtcNow,
            IpAddress      = HttpContext.Connection.RemoteIpAddress?.ToString()
        };

        if (attachment is { Length: > 0 })
        {
            if (!UploadService.IsValidLeadAttachment(attachment, out var attachError))
            {
                var msg = isRtl
                    ? $"المرفق غير صالح: {attachError}"
                    : $"Attachment rejected: {attachError}";
                return RespondError(isAjax, msg);
            }

            var saved = await uploads.SaveLeadAttachmentAsync(attachment);
            if (saved is not null)
            {
                lead.AttachmentUrl  = saved.RelativeUrl;
                lead.AttachmentName = saved.OriginalName;
            }
        }

        try
        {
            _db.LeadRequests.Add(lead);
            await _db.SaveChangesAsync();
            await email.NotifyNewLeadAsync(lead);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save lead request from {Email}", lead.Email);
            var msg = isRtl ? "تعذّر حفظ الطلب. حاول مرة أخرى." : "Could not save your request. Please try again.";
            return RespondError(isAjax, msg);
        }

        var successMsg = isRtl
            ? "شكرًا! تم استلام طلبك وسنعود إليك خلال 48 ساعة."
            : "Thanks! We've received your request and will get back to you within 48 hours.";

        if (isAjax)
            return Json(new { success = true, message = successMsg });

        TempData["LeadSuccess"] = successMsg;
        return Redirect(Url.Action(nameof(Index)) + "#contact");
    }

    private IActionResult RespondError(bool isAjax, string message)
    {
        if (isAjax)
            return Json(new { success = false, message });

        TempData["LeadError"] = message;
        return Redirect(Url.Action(nameof(Index)) + "#contact");
    }

    public IActionResult Error() => View();

    private string ResolveLanguage(string? queryLang)
    {
        if (!string.IsNullOrWhiteSpace(queryLang))
        {
            var normalized = queryLang.ToLowerInvariant();
            if (normalized == "en" || normalized == "ar")
            {
                Response.Cookies.Append(LangCookieName, normalized, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    HttpOnly = false,
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax
                });
                return normalized;
            }
        }

        if (Request.Cookies.TryGetValue(LangCookieName, out var cookieLang))
        {
            var normalized = cookieLang?.ToLowerInvariant();
            if (normalized == "en" || normalized == "ar")
                return normalized;
        }

        return DefaultLanguage;
    }
}
