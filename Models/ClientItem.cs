using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class ClientItem
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(300)]
    public string LogoUrl { get; set; } = string.Empty;

    [StringLength(300)]
    public string? WebsiteUrl { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
