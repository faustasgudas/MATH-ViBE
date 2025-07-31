namespace QuizApi.Domain.Entities;

public class Rating
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid QuizId { get; set; }
    public int Stars { get; set; } = 0;
    public string? Comment { get; set; } = null;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; } = null;
    public Quiz? Quiz { get; set; } = null;
}