using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizApi.Repositories;
using System.ComponentModel.DataAnnotations;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ILogger<QuizzesController> _logger;

        public QuizzesController(IQuizRepository quizRepository, ILogger<QuizzesController> logger)
        {
            _quizRepository = quizRepository;
            _logger = logger;
        }

        // GET: api/quizzes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            try
            {
                var quizzes = await _quizRepository.GetAllAsync();
                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all quizzes");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/quizzes/published
        [HttpGet("published")]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetPublishedQuizzes()
        {
            try
            {
                var quizzes = await _quizRepository.GetPublishedAsync();
                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving published quizzes");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/quizzes/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Quiz>> GetQuiz(Guid id)
        {
            try
            {
                var quiz = await _quizRepository.GetByIdAsync(id);
                
                if (quiz == null)
                {
                    return NotFound($"Quiz with ID {id} not found");
                }

                return Ok(quiz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quiz {QuizId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/quizzes/author/{authorId}
        [HttpGet("author/{authorId:guid}")]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzesByAuthor(Guid authorId)
        {
            try
            {
                var quizzes = await _quizRepository.GetByAuthorIdAsync(authorId);
                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quizzes for author {AuthorId}", authorId);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/quizzes
        [HttpPost]
        public async Task<ActionResult<Quiz>> CreateQuiz(CreateQuizRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var quiz = new Quiz
                {
                    AuthorId = request.AuthorId,
                    Title = request.Title,
                    Topic = request.Topic,
                    Description = request.Description,
                    Difficulty = request.Difficulty,
                    IsPublished = request.IsPublished
                };

                var createdQuiz = await _quizRepository.AddAsync(quiz);
                
                return CreatedAtAction(
                    nameof(GetQuiz), 
                    new { id = createdQuiz.Id }, 
                    createdQuiz
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating quiz");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/quizzes/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Quiz>> UpdateQuiz(Guid id, UpdateQuizRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _quizRepository.ExistsAsync(id))
                {
                    return NotFound($"Quiz with ID {id} not found");
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

                var updatedQuiz = await _quizRepository.UpdateAsync(quiz);
                
                if (updatedQuiz == null)
                {
                    return NotFound($"Quiz with ID {id} not found");
                }

                return Ok(updatedQuiz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating quiz {QuizId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/quizzes/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            try
            {
                var deleted = await _quizRepository.DeleteAsync(id);
                
                if (!deleted)
                {
                    return NotFound($"Quiz with ID {id} not found");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting quiz {QuizId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // PATCH: api/quizzes/{id}/publish
        [HttpPatch("{id:guid}/publish")]
        public async Task<ActionResult<Quiz>> PublishQuiz(Guid id)
        {
            try
            {
                var quiz = await _quizRepository.GetByIdAsync(id);
                if (quiz == null)
                {
                    return NotFound($"Quiz with ID {id} not found");
                }

                quiz.IsPublished = true;
                var updatedQuiz = await _quizRepository.UpdateAsync(quiz);

                return Ok(updatedQuiz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing quiz {QuizId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // PATCH: api/quizzes/{id}/unpublish
        [HttpPatch("{id:guid}/unpublish")]
        public async Task<ActionResult<Quiz>> UnpublishQuiz(Guid id)
        {
            try
            {
                var quiz = await _quizRepository.GetByIdAsync(id);
                if (quiz == null)
                {
                    return NotFound($"Quiz with ID {id} not found");
                }

                quiz.IsPublished = false;
                var updatedQuiz = await _quizRepository.UpdateAsync(quiz);

                return Ok(updatedQuiz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error unpublishing quiz {QuizId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }

    // DTOs for requests
    public class CreateQuizRequest
    {
        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Topic { get; set; } = null!;

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Description { get; set; } = null!;

        [Range(1, 3)]
        public int Difficulty { get; set; } = 1;

        public bool IsPublished { get; set; } = false;
    }

    public class UpdateQuizRequest
    {
        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Topic { get; set; } = null!;

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Description { get; set; } = null!;

        [Range(1, 3)]
        public int Difficulty { get; set; }

        public bool IsPublished { get; set; }
    }
}