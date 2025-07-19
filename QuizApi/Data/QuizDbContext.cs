using Microsoft.EntityFrameworkCore;
using QuizApi.Models;

namespace QuizApi.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Quiz (1:N)
            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Author)
                .WithMany(u => u.CreatedQuizzes)
                .HasForeignKey(q => q.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quiz - Question (1:N)
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(qz => qz.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question - AnswerOption (1:N)
            modelBuilder.Entity<AnswerOption>()
                .HasOne(a => a.Question)
                .WithMany(q => q.AnswerOptions)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - Rating (1:N)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quiz - Rating (1:N)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Quiz)
                .WithMany(q => q.Ratings)
                .HasForeignKey(r => r.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - QuizAttempt (1:N)
            modelBuilder.Entity<QuizAttempt>()
                .HasOne(a => a.User)
                .WithMany(u => u.QuizAttempts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quiz - QuizAttempt (1:N)
            modelBuilder.Entity<QuizAttempt>()
                .HasOne(a => a.Quiz)
                .WithMany(q => q.Attempts)
                .HasForeignKey(a => a.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
