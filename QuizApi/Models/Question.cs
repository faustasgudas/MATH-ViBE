   namespace QuizApi.Models;

    public class Question
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid QuizId { get; set; }
        public string Text { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Explanation { get; set; } = null!;
        public QuestionType Type { get; set; } = QuestionType.SingleChoice; 
        public int Order { get; set; }
    }