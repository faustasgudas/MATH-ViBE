namespace QuizApi.Domain.Entities;

public class AnswerOption
{
    public Guid Id {get; set;} = Guid.NewGuid();
    public Guid QuestionId { get; set; }
    public string Answer { get; set; }
    public bool IsCorrectAnswer { get; set; }
    
    public Question Question { get; set; } = null!;
}