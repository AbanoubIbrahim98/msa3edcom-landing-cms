using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class TestimonialItem
{
    public int Id { get; set; }

    [Required, StringLength(120)] public string NameEn { get; set; } = string.Empty;
    [Required, StringLength(120)] public string NameAr { get; set; } = string.Empty;

    [StringLength(150)] public string? RoleEn { get; set; }
    [StringLength(150)] public string? RoleAr { get; set; }

    [StringLength(150)] public string? CompanyEn { get; set; }
    [StringLength(150)] public string? CompanyAr { get; set; }

    [StringLength(400)] public string? AvatarUrl { get; set; }

    [Required, StringLength(1200)] public string QuoteEn { get; set; } = string.Empty;
    [Required, StringLength(1200)] public string QuoteAr { get; set; } = string.Empty;

    [Range(1, 5)] public int Rating { get; set; } = 5;

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
