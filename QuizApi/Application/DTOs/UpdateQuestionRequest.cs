using System.ComponentModel.DataAnnotations;

namespace QuizApi.Application.DTOs
{
    public class UpdateQuestionRequest
    {
        [Required] public Guid QuizId { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Text { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string? Explanation { get; set; }

        [Required]
        [Range(1, 3)]

        public int Type { get; set; }

        public int Order { get; set; } = 0;
    }
}