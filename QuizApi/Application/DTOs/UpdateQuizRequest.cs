using System.ComponentModel.DataAnnotations;

namespace QuizApi.Application.DTOs
{
    public class UpdateQuizRequest
    {
        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Description { get; set; } = string.Empty;

        [Range(1, 3)]
        public int Difficulty { get; set; }

        public bool IsPublished { get; set; }
    }
}