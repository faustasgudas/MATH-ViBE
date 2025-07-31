using QuizApi.Domain.Entities;

namespace QuizApi.Application.Interfaces.Persistence
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetAllAsync();
        Task<Quiz?> GetByIdAsync(Guid id);
        Task<Quiz> AddAsync(Quiz quiz);
        Task<bool> DeleteAsync(Guid id);
        Task<Quiz?> UpdateAsync(Quiz quiz);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Quiz>> GetByAuthorIdAsync(Guid authorId);
        Task<IEnumerable<Quiz>> GetPublishedAsync();
    }
}