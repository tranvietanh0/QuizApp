using Dapper;
using QuizAppBackend.Data.Repositories.Interfaces;
using QuizAppBackend.DTOs.Leaderboard;

namespace QuizAppBackend.Data.Repositories;

public class LeaderboardRepository : ILeaderboardRepository
{
    private readonly DbConnectionFactory _dbFactory;

    public LeaderboardRepository(DbConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<LeaderboardEntryDto>> GetGlobalLeaderboardAsync(int top = 50)
    {
        using var connection = _dbFactory.CreateConnection();
        return await connection.QueryAsync<LeaderboardEntryDto>(
            @"SELECT
                ROW_NUMBER() OVER (ORDER BY SUM(BestScore) DESC) AS `Rank`,
                UserId,
                DisplayName,
                SUM(BestScore) AS TotalScore,
                COUNT(*) AS QuizzesCompleted
              FROM (
                SELECT qa.UserId, u.DisplayName, qa.QuizId, MAX(qa.Score) AS BestScore
                FROM QuizAttempts qa
                INNER JOIN Users u ON qa.UserId = u.Id
                GROUP BY qa.UserId, u.DisplayName, qa.QuizId
              ) AS UserBestScores
              GROUP BY UserId, DisplayName
              ORDER BY TotalScore DESC
              LIMIT @Top",
            new { Top = top });
    }

    public async Task<IEnumerable<LeaderboardEntryDto>> GetQuizLeaderboardAsync(int quizId, int top = 50)
    {
        using var connection = _dbFactory.CreateConnection();
        return await connection.QueryAsync<LeaderboardEntryDto>(
            @"SELECT
                ROW_NUMBER() OVER (ORDER BY MAX(qa.Score) DESC) AS `Rank`,
                qa.UserId,
                u.DisplayName,
                MAX(qa.Score) AS TotalScore,
                COUNT(*) AS QuizzesCompleted
              FROM QuizAttempts qa
              INNER JOIN Users u ON qa.UserId = u.Id
              WHERE qa.QuizId = @QuizId
              GROUP BY qa.UserId, u.DisplayName
              ORDER BY TotalScore DESC
              LIMIT @Top",
            new { QuizId = quizId, Top = top });
    }
}
