namespace QuizApi.Domain.Entities;

public class QuizAttempt
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid QuizId { get; set; }
    public decimal Score { get; set; }
    public DateTime Started { get; set; } = DateTime.UtcNow;
    public DateTime? Completed { get; set; } = DateTime.UtcNow;
    
    public Quiz? Quiz { get; set; } = null!;
    public User? User { get; set; } = null!;
    
}