using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class LeadRequest
{
    public int Id { get; set; }

    [Required, StringLength(150)] public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(200)] public string Email { get; set; } = string.Empty;

    [StringLength(40)] public string? PhoneNumber { get; set; }
    [StringLength(40)] public string? WhatsAppNumber { get; set; }
    [StringLength(150)] public string? CompanyName { get; set; }

    [StringLength(80)] public string? ServiceType { get; set; }
    [StringLength(80)] public string? Budget { get; set; }

    [Required, StringLength(4000)] public string Message { get; set; } = string.Empty;

    [StringLength(400)] public string? AttachmentUrl { get; set; }
    [StringLength(200)] public string? AttachmentName { get; set; }

    [Required, StringLength(20)] public string Status { get; set; } = LeadStatus.New;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    [StringLength(45)] public string? IpAddress { get; set; }
}

public static class LeadStatus
{
    public const string New        = "New";
    public const string Contacted  = "Contacted";
    public const string InProgress = "In Progress";
    public const string Closed     = "Closed";

    public static readonly string[] All = { New, Contacted, InProgress, Closed };

    public static string CssClass(string status) => status switch
    {
        New        => "lead-status-new",
        Contacted  => "lead-status-contacted",
        InProgress => "lead-status-progress",
        Closed     => "lead-status-closed",
        _          => "lead-status-new"
    };
}

public static class LeadServiceTypes
{
    public static readonly (string Value, string En, string Ar)[] All =
    {
        ("website",       "Website Development",  "تطوير موقع إلكتروني"),
        ("mobile",        "Mobile App",           "تطبيق هاتف"),
        ("government",    "Government System",    "نظام حكومي"),
        ("erp",           "ERP System",           "نظام ERP"),
        ("uiux",          "UI/UX Design",         "تصميم واجهات"),
        ("api",           "API Integration",      "تكامل واجهات API"),
        ("consultation",  "Consultation",         "استشارة"),
        ("other",         "Other",                "أخرى"),
    };

    public static string? DisplayLabel(string? value, bool isRtl)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        var match = All.FirstOrDefault(x => x.Value == value);
        if (match.Value is null) return value;
        return isRtl ? match.Ar : match.En;
    }
}

public static class LeadBudgets
{
    public static readonly (string Value, string En, string Ar)[] All =
    {
        ("under-5k",    "Under $5,000",     "أقل من 5,000$"),
        ("5k-15k",      "$5,000 – $15,000", "5,000$ – 15,000$"),
        ("15k-50k",     "$15,000 – $50,000","15,000$ – 50,000$"),
        ("50k-plus",    "$50,000+",         "50,000$ فأكثر"),
        ("undecided",   "Not sure yet",     "غير محدد بعد"),
    };
}
