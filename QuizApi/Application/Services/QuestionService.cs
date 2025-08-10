using QuizApi.Domain.Entities;
using QuizApi.Application.DTOs;
using QuizApi.Application.Interfaces.Persistence;  
using QuizApi.Application.Interfaces.Services;
using QuizApi.Domain.Enums;

namespace QuizApi.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<QuestionService> _logger;

        public QuestionService(IQuestionRepository questionRepository, ILogger<QuestionService> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _questionRepository.GetAllAsync();
        }

        public async Task<Question?> GetByIdAsync(Guid id)
        {
            return await _questionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Question>> GetByQuizIdAsync(Guid id)
        {
            return await _questionRepository.GetByQuizIdAsync(id);
        }

        public async Task<Question?> CreateAsync(CreateQuestionRequest request)
        {
            try
            {
                var question = new Question
                {
                    Id = Guid.NewGuid(),
                    QuizId = request.QuizId,
                    Text = request.Text,
                    ImageUrl = request.ImageUrl,
                    Explanation = request.Explanation,
                    Type = (QuestionType)request.Type,
                    Order = request.Order

                };
                return await _questionRepository.AddAsync(question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating question");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _questionRepository.DeleteAsync(id);
        }

        public async Task<Question?> UpdateAsync(Guid id, UpdateQuestionRequest request)
        {
            var existingQuestion = await _questionRepository.GetByIdAsync(id);
            if (existingQuestion == null)
            {
                _logger.LogWarning("Question with ID {QuestionId} not found.", id);
                return null;
            }

            // Apply updated values from the request
            existingQuestion.QuizId = request.QuizId;
            existingQuestion.Text = request.Text;
            existingQuestion.ImageUrl = request.ImageUrl;
            existingQuestion.Explanation = request.Explanation;
            existingQuestion.Type = (QuestionType)request.Type;
            existingQuestion.Order = request.Order;

            // Save updated entity to DB
            return await _questionRepository.UpdateAsync(existingQuestion);
        }



    }
}