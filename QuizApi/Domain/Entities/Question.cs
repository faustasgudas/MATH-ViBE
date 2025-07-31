   using QuizApi.Domain.Enums;

   namespace QuizApi.Domain.Entities;

    public class Question
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid QuizId { get; set; }
        public string Text { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Explanation { get; set; } = null!;
        public QuestionType Type { get; set; } = QuestionType.SingleChoice; 
        public int Order { get; set; }
        
        public Quiz Quiz { get; set; } = null!;
        public List<AnswerOption> AnswerOptions { get; set; } = new();
    }