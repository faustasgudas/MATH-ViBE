using Microsoft.AspNetCore.Mvc;
using QuizApi.Application.DTOs;
using QuizApi.Application.Interfaces.Services;
using QuizApi.Domain.Entities;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            var quizzes = await _quizService.GetAllAsync();
            return Ok(quizzes);
        }

        [HttpGet("published")]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetPublishedQuizzes()
        {
            var quizzes = await _quizService.GetPublishedAsync();
            return Ok(quizzes);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Quiz>> GetQuiz(Guid id)
        {
            var quiz = await _quizService.GetByIdAsync(id);
            
            if (quiz == null)
                return NotFound($"Quiz with ID {id} not found");

            return Ok(quiz);
        }

        [HttpGet("author/{authorId:guid}")]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzesByAuthor(Guid authorId)
        {
            var quizzes = await _quizService.GetByAuthorIdAsync(authorId);
            return Ok(quizzes);
        }

        [HttpPost]
        public async Task<ActionResult<Quiz>> CreateQuiz(CreateQuizRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var quiz = await _quizService.CreateAsync(request);
            
            if (quiz == null)
                return BadRequest("Failed to create quiz");

            return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, quiz);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Quiz>> UpdateQuiz(Guid id, UpdateQuizRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var quiz = await _quizService.UpdateAsync(id, request);
            
            if (quiz == null)
                return NotFound($"Quiz with ID {id} not found");

            return Ok(quiz);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            var deleted = await _quizService.DeleteAsync(id);
            
            if (!deleted)
                return NotFound($"Quiz with ID {id} not found");

            return NoContent();
        }

        [HttpPatch("{id:guid}/publish")]
        public async Task<ActionResult<Quiz>> PublishQuiz(Guid id)
        {
            var quiz = await _quizService.PublishAsync(id);
            
            if (quiz == null)
                return NotFound($"Quiz with ID {id} not found or cannot be published");

            return Ok(quiz);
        }

        [HttpPatch("{id:guid}/unpublish")]
        public async Task<ActionResult<Quiz>> UnpublishQuiz(Guid id)
        {
            var quiz = await _quizService.UnpublishAsync(id);
            
            if (quiz == null)
                return NotFound($"Quiz with ID {id} not found");

            return Ok(quiz);
        }
    }
}