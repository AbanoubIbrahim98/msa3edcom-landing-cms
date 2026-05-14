using Msa3edcomAdmin.Models;

namespace Msa3edcomAdmin.ViewModels;

public class LandingPageViewModel
{
    public SiteContent  Content  { get; set; } = new();
    public SiteSettings Settings { get; set; } = new();

    public List<ClientItem>      Clients      { get; set; } = new();
    public List<ServiceItem>     Services     { get; set; } = new();
    public List<PortfolioItem>   Portfolio    { get; set; } = new();
    public List<TestimonialItem> Testimonials { get; set; } = new();
    public List<FaqItem>         Faqs         { get; set; } = new();
    public List<TechStackItem>   TechStack    { get; set; } = new();
    public List<StatItem>        Stats        { get; set; } = new();
    public List<ProcessStep>     Process      { get; set; } = new();

    public string Language { get; set; } = "ar";
    public bool   IsRtl     => Language == "ar";
    public string Direction => IsRtl ? "rtl" : "ltr";
}
