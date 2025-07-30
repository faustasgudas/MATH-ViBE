using QuizApi.Models;

namespace QuizApi.Repositories;
public interface IQuestionRepository
{
    Task<IEnumerable<Question>> GetAllAsync();
    Task<Question?> GetByIdAsync(Guid id);
    Task<IEnumerable<Question>> GetByQuizIdAsync(Guid quizId);
    Task<Question> AddAsync(Question question);
    Task<Question?> UpdateAsync(Question question);
    Task<bool> DeleteAsync(Guid id);
}
