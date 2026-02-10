using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizAppBackend.DTOs.Quiz;
using QuizAppBackend.Extensions;
using QuizAppBackend.Services.Interfaces;

namespace QuizAppBackend.Controllers;

[ApiController]
[Route("api/quizzes")]
[Authorize]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuizListItemDto>>> GetAll()
    {
        var quizzes = await _quizService.GetAllQuizzesAsync();
        return Ok(quizzes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QuizDetailDto>> GetById(int id)
    {
        var quiz = await _quizService.GetQuizDetailAsync(id);
        return Ok(quiz);
    }

    [HttpPost("{id}/submit")]
    public async Task<ActionResult<SubmitQuizResponse>> Submit(int id, [FromBody] SubmitQuizRequest request)
    {
        var userId = User.GetUserId();
        var response = await _quizService.SubmitQuizAsync(id, userId, request);
        return Ok(response);
    }
}
