using Microsoft.AspNetCore.Mvc;
using QuizApi.Application.DTOs;
using QuizApi.Application.Interfaces.Services;
using QuizApi.Domain.Entities;


namespace QuizApi.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(IQuestionService questionService, ILogger<QuestionsController> logger)
        {
            _questionService = questionService;
            _logger = logger;
        }
        
        // GET: api/questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            try
            {
                var questions = await _questionService.GetAllAsync();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all questions");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Question>> GetQuestion(Guid id)
        {
            var question = await _questionService.GetByIdAsync(id);
            if (question == null)
                return NotFound($"Question with ID {id} not found");
            return Ok(question);
        }

        [HttpGet("quiz/{quizId:guid}")]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsByQuizId(Guid quizId)
        {
            var questions = await _questionService.GetByQuizIdAsync(quizId);
            if (questions == null || !questions.Any())
                return NotFound($"No questions found for quiz ID {quizId}");
            return Ok(questions);
        }

        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion(CreateQuestionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var question = await _questionService.CreateAsync(request);
            
            if (question == null)
                return BadRequest($"Failed to create question");
            
            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Question>> UpdateQuestion(Guid id, UpdateQuestionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var question = await _questionService.UpdateAsync(id, request);
            
            if (question == null)
                return NotFound($"Question with ID {id} not found");
            
            return Ok(question);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            var deleted = await _questionService.DeleteAsync(id);
            
            if (!deleted)
                return NotFound($"Question with ID {id} not found");
            
            return NoContent();
        }
        
        


    }
    
}





