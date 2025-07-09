namespace QuizApi.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = "student";
    public DateTime Created { get; set; } =  DateTime.UtcNow;;
    public int Xp { get; set; } = 0;
}