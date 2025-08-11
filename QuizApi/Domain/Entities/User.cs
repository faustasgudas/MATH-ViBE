using Microsoft.AspNetCore.Identity;
using QuizApi.Domain.Enums;

namespace QuizApi.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public UserRole Role { get; set; } = UserRole.Student;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public int Xp { get; set; } = 0;

        public List<Quiz> CreatedQuizzes { get; set; } = new List<Quiz>();
        public List<QuizAttempt> QuizAttempts { get; set; } = new();
        public List<Rating> Ratings { get; set; } = new();
    }
}