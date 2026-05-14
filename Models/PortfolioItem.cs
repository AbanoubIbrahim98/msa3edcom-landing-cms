using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class PortfolioItem
{
    public int Id { get; set; }

    [Required, StringLength(150)] public string TitleEn { get; set; } = string.Empty;
    [Required, StringLength(150)] public string TitleAr { get; set; } = string.Empty;

    [StringLength(1000)] public string? DescriptionEn { get; set; }
    [StringLength(1000)] public string? DescriptionAr { get; set; }

    [StringLength(400)] public string? ThumbnailUrl { get; set; }

    [StringLength(400)] public string? Technologies { get; set; } // comma-separated

    [StringLength(400)] public string? ProjectUrl { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public IEnumerable<string> TechList =>
        string.IsNullOrWhiteSpace(Technologies)
            ? Array.Empty<string>()
            : Technologies.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}
