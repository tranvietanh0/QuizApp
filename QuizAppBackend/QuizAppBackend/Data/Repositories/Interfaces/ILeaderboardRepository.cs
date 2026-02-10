using QuizAppBackend.DTOs.Leaderboard;

namespace QuizAppBackend.Data.Repositories.Interfaces;

public interface ILeaderboardRepository
{
    Task<IEnumerable<LeaderboardEntryDto>> GetGlobalLeaderboardAsync(int top = 50);
    Task<IEnumerable<LeaderboardEntryDto>> GetQuizLeaderboardAsync(int quizId, int top = 50);
}
