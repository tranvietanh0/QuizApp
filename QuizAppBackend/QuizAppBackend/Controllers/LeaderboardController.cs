using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizAppBackend.DTOs.Leaderboard;
using QuizAppBackend.Services.Interfaces;

namespace QuizAppBackend.Controllers;

[ApiController]
[Route("api/leaderboard")]
[Authorize]
public class LeaderboardController : ControllerBase
{
    private readonly ILeaderboardService _leaderboardService;

    public LeaderboardController(ILeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeaderboardEntryDto>>> GetGlobal()
    {
        var leaderboard = await _leaderboardService.GetGlobalLeaderboardAsync();
        return Ok(leaderboard);
    }

    [HttpGet("quiz/{quizId}")]
    public async Task<ActionResult<IEnumerable<LeaderboardEntryDto>>> GetByQuiz(int quizId)
    {
        var leaderboard = await _leaderboardService.GetQuizLeaderboardAsync(quizId);
        return Ok(leaderboard);
    }
}
