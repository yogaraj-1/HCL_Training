
using System.ComponentModel.DataAnnotations;

namespace OnlineExaminationQuestionAPI.Models;

public class Question
{
    public int Id { get; set; }

    [Required]
    public string Text { get; set; } = string.Empty;

    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public string OptionA { get; set; } = string.Empty;

    [Required]
    public string OptionB { get; set; } = string.Empty;

    [Required]
    public string OptionC { get; set; } = string.Empty;

    [Required]
    public string OptionD { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^[ABCD]$",
        ErrorMessage = "CorrectOption must be A, B, C, or D.")]
    public string CorrectOption { get; set; } = string.Empty;
}
