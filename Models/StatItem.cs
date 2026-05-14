using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class StatItem
{
    public int Id { get; set; }

    [Required, StringLength(80)] public string LabelEn { get; set; } = string.Empty;
    [Required, StringLength(80)] public string LabelAr { get; set; } = string.Empty;

    [Required, StringLength(20)] public string Value { get; set; } = "0";

    [StringLength(10)] public string? Suffix { get; set; }

    [StringLength(80)] public string? IconClass { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
