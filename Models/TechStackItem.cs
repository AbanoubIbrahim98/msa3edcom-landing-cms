using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class TechStackItem
{
    public int Id { get; set; }

    [Required, StringLength(80)] public string Name { get; set; } = string.Empty;

    [StringLength(400)] public string? IconUrl { get; set; }
    [StringLength(80)]  public string? IconClass { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
