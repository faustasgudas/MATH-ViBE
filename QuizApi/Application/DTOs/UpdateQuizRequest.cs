namespace QuizApi.Application.DTOs;

public class UpdateQuizRequest
{
        public Guid AuthorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Difficulty { get; set; } // 1–3 (Easy, Medium, Hard)
        public bool IsPublished { get; set; }
}