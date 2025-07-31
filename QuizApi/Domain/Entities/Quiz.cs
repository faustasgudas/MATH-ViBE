namespace QuizApi.Domain.Entities;

public class Quiz
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AuthorId { get; set; }
    public string Title { get; set; } = null!;
    public string Topic { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Difficulty { get; set; } // 1-3 (Easy, Medium, Hard)
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public Boolean IsPublished { get; set; } = false;
    
    public User Author { get; set; } = null!;
    public List<Question> Questions { get; set; } = new();
    public List<Rating> Ratings { get; set; } = new();
    public List<QuizAttempt> Attempts { get; set; } = new();
}