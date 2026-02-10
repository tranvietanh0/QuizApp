using QuizAppBackend.Data.Repositories.Interfaces;
using QuizAppBackend.DTOs.Leaderboard;
using QuizAppBackend.Services.Interfaces;

namespace QuizAppBackend.Services;

public class LeaderboardService : ILeaderboardService
{
    private readonly ILeaderboardRepository _leaderboardRepository;

    public LeaderboardService(ILeaderboardRepository leaderboardRepository)
    {
        _leaderboardRepository = leaderboardRepository;
    }

    public async Task<IEnumerable<LeaderboardEntryDto>> GetGlobalLeaderboardAsync()
    {
        return await _leaderboardRepository.GetGlobalLeaderboardAsync();
    }

    public async Task<IEnumerable<LeaderboardEntryDto>> GetQuizLeaderboardAsync(int quizId)
    {
        return await _leaderboardRepository.GetQuizLeaderboardAsync(quizId);
    }
}
