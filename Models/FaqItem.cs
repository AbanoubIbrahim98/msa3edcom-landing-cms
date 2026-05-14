using System.ComponentModel.DataAnnotations;

namespace Msa3edcomAdmin.Models;

public class FaqItem
{
    public int Id { get; set; }

    [Required, StringLength(300)] public string QuestionEn { get; set; } = string.Empty;
    [Required, StringLength(300)] public string QuestionAr { get; set; } = string.Empty;

    [Required, StringLength(2000)] public string AnswerEn { get; set; } = string.Empty;
    [Required, StringLength(2000)] public string AnswerAr { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
