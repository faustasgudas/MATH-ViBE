using System.ComponentModel.DataAnnotations;

namespace QuizApi.Application.DTOs;

public class CreateQuestionRequest
{
    [Required]
    public Guid QuizId { get; set; }
    [Required]
    [StringLength(300, MinimumLength = 1)]
    public string Text { get; set; }
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    [StringLength(500)]
    public string? Explanation { get; set; }
    [Required]
    [Range(1, 3)]
    public int Type { get; set; }
    public int Order { get; set; } = 0;

}