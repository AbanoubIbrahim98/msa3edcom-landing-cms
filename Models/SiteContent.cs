using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class SiteContent
{
    public int Id { get; set; }

    // ---------- General Settings ----------
    [StringLength(400)] public string? LogoUrl { get; set; }
    [StringLength(400)] public string? LogoDarkUrl { get; set; }
    [StringLength(400)] public string? FaviconUrl { get; set; }
    [StringLength(40)]  public string? Phone { get; set; }
    [StringLength(40)]  public string? WhatsApp { get; set; }
    [EmailAddress, StringLength(200)] public string? Email { get; set; }
    [StringLength(300)] public string? Address { get; set; }

    [StringLength(500)] public string? FooterTextEn { get; set; }
    [StringLength(500)] public string? FooterTextAr { get; set; }

    [StringLength(300)] public string? LinkedInUrl { get; set; }
    [StringLength(300)] public string? TwitterUrl { get; set; }
    [StringLength(300)] public string? GitHubUrl { get; set; }
    [StringLength(300)] public string? InstagramUrl { get; set; }
    [StringLength(300)] public string? FacebookUrl { get; set; }

    // ---------- Hero Section ----------
    [StringLength(300)] public string? HeroEyebrowEn { get; set; }
    [StringLength(300)] public string? HeroEyebrowAr { get; set; }
    [StringLength(300)] public string? HeroTitleEn { get; set; }
    [StringLength(300)] public string? HeroTitleAr { get; set; }
    [StringLength(800)] public string? HeroSubtitleEn { get; set; }
    [StringLength(800)] public string? HeroSubtitleAr { get; set; }
    [StringLength(400)] public string? HeroImageUrl { get; set; }
    [StringLength(80)]  public string? HeroPrimaryCtaTextEn { get; set; }
    [StringLength(80)]  public string? HeroPrimaryCtaTextAr { get; set; }
    [StringLength(300)] public string? HeroPrimaryCtaUrl { get; set; }
    [StringLength(80)]  public string? HeroSecondaryCtaTextEn { get; set; }
    [StringLength(80)]  public string? HeroSecondaryCtaTextAr { get; set; }
    [StringLength(300)] public string? HeroSecondaryCtaUrl { get; set; }

    // ---------- About Section ----------
    [StringLength(200)]  public string? AboutTitleEn { get; set; }
    [StringLength(200)]  public string? AboutTitleAr { get; set; }
    [StringLength(2000)] public string? AboutTextEn { get; set; }
    [StringLength(2000)] public string? AboutTextAr { get; set; }
    [StringLength(400)]  public string? AboutImageUrl { get; set; }

    // ---------- Services Section Header ----------
    [StringLength(200)] public string? ServicesTitleEn { get; set; }
    [StringLength(200)] public string? ServicesTitleAr { get; set; }
    [StringLength(500)] public string? ServicesLeadEn { get; set; }
    [StringLength(500)] public string? ServicesLeadAr { get; set; }

    // ---------- Portfolio Section Header ----------
    [StringLength(200)] public string? PortfolioTitleEn { get; set; }
    [StringLength(200)] public string? PortfolioTitleAr { get; set; }
    [StringLength(500)] public string? PortfolioLeadEn { get; set; }
    [StringLength(500)] public string? PortfolioLeadAr { get; set; }

    // ---------- CTA Banner ----------
    [StringLength(200)] public string? CtaBannerTitleEn { get; set; }
    [StringLength(200)] public string? CtaBannerTitleAr { get; set; }
    [StringLength(500)] public string? CtaBannerSubtitleEn { get; set; }
    [StringLength(500)] public string? CtaBannerSubtitleAr { get; set; }
    [StringLength(80)]  public string? CtaBannerButtonTextEn { get; set; }
    [StringLength(80)]  public string? CtaBannerButtonTextAr { get; set; }
    [StringLength(300)] public string? CtaBannerButtonUrl { get; set; }
    [StringLength(400)] public string? CtaBannerImageUrl { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
