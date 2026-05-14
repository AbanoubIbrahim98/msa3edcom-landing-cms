using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class SiteSettings
{
    public int Id { get; set; }

    // ---------- SEO ----------
    [StringLength(160)] public string? MetaTitleEn { get; set; }
    [StringLength(160)] public string? MetaTitleAr { get; set; }
    [StringLength(320)] public string? MetaDescriptionEn { get; set; }
    [StringLength(320)] public string? MetaDescriptionAr { get; set; }
    [StringLength(320)] public string? MetaKeywordsEn { get; set; }
    [StringLength(320)] public string? MetaKeywordsAr { get; set; }
    [StringLength(400)] public string? OpenGraphImage { get; set; }
    [StringLength(400)] public string? FaviconUrl { get; set; }

    // ---------- Analytics ----------
    [StringLength(60)] public string? GoogleAnalyticsCode { get; set; }

    // ---------- Branding ----------
    [StringLength(20)] public string? PrimaryColor { get; set; }
    [StringLength(20)] public string? SecondaryColor { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
