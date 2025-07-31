using System.ComponentModel.DataAnnotations;

namespace QuizApi.Application.DTOs
{
    public class CreateQuizRequest
    {
        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Topic { get; set; } = null!;

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Description { get; set; } = null!;

        [Range(1, 3)]
        public int Difficulty { get; set; } = 1;

        public bool IsPublished { get; set; } = false;
    }
}