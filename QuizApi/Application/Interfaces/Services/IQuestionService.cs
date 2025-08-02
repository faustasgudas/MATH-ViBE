using QuizApi.Domain.Entities;
using QuizApi.Application.DTOs;

namespace QuizApi.Application.Interfaces.Services;

public interface IQuestionService
{
    Task<IEnumerable<Question>> GetAllAsync();
    Task<Question> GetByIdAsync(Guid id);
    Task<IEnumerable<Question>> GetByQuizIdAsync(Guid id);
    Task<Question?> CreateAsync(CreateQuestionRequest request);
    Task<Question?> UpdateAsync(Guid id, UpdateQuestionRequest request);
    Task<bool> DeleteAsync(Guid id);


}