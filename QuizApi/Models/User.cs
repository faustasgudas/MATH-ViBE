namespace QuizApi.Models;

public class User
{
    public Guid id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } = null!;
    public string Roll { get; set; } = "student";
    public DateTime Created { get; set; } =  DateTime.Now;
    public int Xp { get; set; } = 0;
}