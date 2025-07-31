using QuizApi.Domain.Entities;
using QuizApi.Application.DTOs;
using QuizApi.Application.Interfaces.Persistence;  
using QuizApi.Application.Interfaces.Services;

namespace QuizApi.Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ILogger<QuizService> _logger;

        public QuizService(IQuizRepository quizRepository, ILogger<QuizService> logger)
        {
            _quizRepository = quizRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await _quizRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Quiz>> GetPublishedAsync()
        {
            return await _quizRepository.GetPublishedAsync();
        }

        public async Task<Quiz?> GetByIdAsync(Guid id)
        {
            return await _quizRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Quiz>> GetByAuthorIdAsync(Guid authorId)
        {
            return await _quizRepository.GetByAuthorIdAsync(authorId);
        }

        public async Task<Quiz?> CreateAsync(CreateQuizRequest request)
        {
            try
            {
                var quiz = new Quiz
                {
                    Id = Guid.NewGuid(),
                    AuthorId = request.AuthorId,
                    Title = request.Title,
                    Topic = request.Topic,
                    Description = request.Description,
                    Difficulty = request.Difficulty,
                    IsPublished = request.IsPublished,
                    Created = DateTime.UtcNow
                };

                return await _quizRepository.AddAsync(quiz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating quiz for author {AuthorId}", request.AuthorId);
                return null;
            }
        }

        public async Task<Quiz?> UpdateAsync(Guid id, UpdateQuizRequest request)
        {
            try
            {
                if (!await _quizRepository.ExistsAsync(id))
                {
                    return null;
                }

                var quiz = new Quiz
                {
                    Id = id,
                    AuthorId = request.AuthorId,
                    Title = request.Title,
                    Topic = request.Topic,
                    Description = request.Description,
                    Difficulty = request.Difficulty,
                    IsPublished = request.IsPublished
                };

                return await _quizRepository.UpdateAsync(quiz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating quiz {QuizId}", id);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _quizRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting quiz {QuizId}", id);
                return false;
            }
        }

        public async Task<Quiz?> PublishAsync(Guid id)
        {
            try
            {
                var quiz = await _quizRepository.GetByIdAsync(id);
                if (quiz == null) return null;

                // Business logic - pvz. tikrinti ar quiz turi klausimų
                if (quiz.Questions == null || !quiz.Questions.Any())
                {
                    _logger.LogWarning("Cannot publish quiz {QuizId} - no questions", id);
                    return null;
                }

                quiz.IsPublished = true;
                return await _quizRepository.UpdateAsync(quiz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing quiz {QuizId}", id);
                return null;
            }
        }

        public async Task<Quiz?> UnpublishAsync(Guid id)
        {
            try
            {
                var quiz = await _quizRepository.GetByIdAsync(id);
                if (quiz == null) return null;

                quiz.IsPublished = false;
                return await _quizRepository.UpdateAsync(quiz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error unpublishing quiz {QuizId}", id);
                return null;
            }
        }
    }
}