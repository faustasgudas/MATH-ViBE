using Microsoft.EntityFrameworkCore;
using QuizApi.Data;
using QuizApi.Models;

namespace QuizApi.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizDbContext _context;

        public QuizRepository(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await _context.Quizzes
                .Include(q => q.Author)
                .Include(q => q.Questions)
                .Include(q => q.Ratings)
                .Include(q => q.Attempts)
                .OrderByDescending(q => q.Created)
                .ToListAsync();
        }

        public async Task<Quiz?> GetByIdAsync(Guid id)
        {
            return await _context.Quizzes
                .Include(q => q.Author)
                .Include(q => q.Questions)
                .Include(q => q.Ratings)
                .Include(q => q.Attempts)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Quiz>> GetByAuthorIdAsync(Guid authorId)
        {
            return await _context.Quizzes
                .Include(q => q.Author)
                .Include(q => q.Questions)
                .Include(q => q.Ratings)
                .Where(q => q.AuthorId == authorId)
                .OrderByDescending(q => q.Created)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quiz>> GetPublishedAsync()
        {
            return await _context.Quizzes
                .Include(q => q.Author)
                .Include(q => q.Questions)
                .Include(q => q.Ratings)
                .Where(q => q.IsPublished == true)
                .OrderByDescending(q => q.Created)
                .ToListAsync();
        }

        public async Task<Quiz> AddAsync(Quiz quiz)
        {
            if (quiz.Id == Guid.Empty)
                quiz.Id = Guid.NewGuid();
                
            quiz.Created = DateTime.UtcNow;
            
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            
            return await GetByIdAsync(quiz.Id) ?? quiz;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null) return false;

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Quiz?> UpdateAsync(Quiz quiz)
        {
            var existing = await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.Id == quiz.Id);
                
            if (existing == null) return null;

            // Update properties
            existing.Title = quiz.Title;
            existing.Topic = quiz.Topic;
            existing.Description = quiz.Description;
            existing.Difficulty = quiz.Difficulty;
            existing.IsPublished = quiz.IsPublished;

            try
            {
                await _context.SaveChangesAsync();
                return await GetByIdAsync(existing.Id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsAsync(quiz.Id))
                {
                    return null;
                }
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Quizzes.AnyAsync(q => q.Id == id);
        }
    }
}
