using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class MediaItem
{
    public int Id { get; set; }

    [Required, StringLength(260)]
    public string FileName { get; set; } = string.Empty;

    [Required, StringLength(400)]
    public string FileUrl { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string FileType { get; set; } = string.Empty;

    [Required, StringLength(40)]
    public string Category { get; set; } = MediaCategories.Default;

    [StringLength(200)]
    public string? AltText { get; set; }

    public long SizeBytes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public static class MediaCategories
{
    public const string Logos = "logos";
    public const string Hero = "hero";
    public const string About = "about";
    public const string Services = "services";
    public const string Clients = "clients";
    public const string Portfolio = "portfolio";
    public const string Icons = "icons";
    public const string Backgrounds = "backgrounds";

    public const string Default = "general";

    public static readonly string[] All =
    {
        Logos, Hero, About, Services, Clients, Portfolio, Icons, Backgrounds
    };
}
