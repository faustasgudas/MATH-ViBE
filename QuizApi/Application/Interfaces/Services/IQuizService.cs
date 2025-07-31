using QuizApi.Domain.Entities;
using QuizApi.Application.DTOs;

namespace QuizApi.Application.Interfaces.Services
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> GetAllAsync();
        Task<IEnumerable<Quiz>> GetPublishedAsync();
        Task<Quiz?> GetByIdAsync(Guid id);
        Task<IEnumerable<Quiz>> GetByAuthorIdAsync(Guid authorId);
        Task<Quiz?> CreateAsync(CreateQuizRequest request);
        Task<Quiz?> UpdateAsync(Guid id, UpdateQuizRequest request);
        Task<bool> DeleteAsync(Guid id);
        Task<Quiz?> PublishAsync(Guid id);
        Task<Quiz?> UnpublishAsync(Guid id);
    }
}