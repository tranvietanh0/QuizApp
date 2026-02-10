using QuizAppBackend.DTOs.Leaderboard;

namespace QuizAppBackend.Services.Interfaces;

public interface ILeaderboardService
{
    Task<IEnumerable<LeaderboardEntryDto>> GetGlobalLeaderboardAsync();
    Task<IEnumerable<LeaderboardEntryDto>> GetQuizLeaderboardAsync(int quizId);
}
