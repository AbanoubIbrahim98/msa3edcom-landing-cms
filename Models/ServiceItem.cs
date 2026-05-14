using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class ServiceItem
{
    public int Id { get; set; }

    [Required, StringLength(150)] public string TitleEn { get; set; } = string.Empty;
    [Required, StringLength(150)] public string TitleAr { get; set; } = string.Empty;

    [StringLength(800)] public string? DescriptionEn { get; set; }
    [StringLength(800)] public string? DescriptionAr { get; set; }

    [StringLength(400)] public string? IconUrl { get; set; }
    [StringLength(80)]  public string? IconClass { get; set; }
    [StringLength(400)] public string? ImageUrl { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
