using Microsoft.AspNetCore.Mvc;
using QuizApi.Domain.Entities;
using QuizApi.Application.Interfaces.Persistence;

namespace QuizApi.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(IQuestionRepository questionRepository, ILogger<QuestionsController> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }
        
        // GET: api/questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            try
            {
                var questions = await _questionRepository.GetAllAsync();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all questions");
                return StatusCode(500, "Internal server error");
            }
        }
        
    }
    
}





