using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class ProcessStep
{
    public int Id { get; set; }

    [Required, StringLength(120)] public string TitleEn { get; set; } = string.Empty;
    [Required, StringLength(120)] public string TitleAr { get; set; } = string.Empty;

    [StringLength(500)] public string? DescriptionEn { get; set; }
    [StringLength(500)] public string? DescriptionAr { get; set; }

    [StringLength(80)] public string? IconClass { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
