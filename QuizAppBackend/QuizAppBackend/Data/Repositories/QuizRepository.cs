using Dapper;
using QuizAppBackend.Data.Repositories.Interfaces;
using QuizAppBackend.DTOs.Quiz;
using QuizAppBackend.Models;

namespace QuizAppBackend.Data.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly DbConnectionFactory _dbFactory;

    public QuizRepository(DbConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<QuizListItemDto>> GetAllActiveAsync()
    {
        using var connection = _dbFactory.CreateConnection();
        return await connection.QueryAsync<QuizListItemDto>(
            @"SELECT q.Id, q.Title, q.Description, q.Category, q.TimeLimitSeconds,
                     COUNT(qu.Id) AS QuestionCount
              FROM Quizzes q
              LEFT JOIN Questions qu ON qu.QuizId = q.Id
              WHERE q.IsActive = 1
              GROUP BY q.Id, q.Title, q.Description, q.Category, q.TimeLimitSeconds
              ORDER BY q.Id");
    }

    public async Task<Quiz?> GetByIdAsync(int id)
    {
        using var connection = _dbFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Quiz>(
            "SELECT * FROM Quizzes WHERE Id = @Id AND IsActive = 1",
            new { Id = id });
    }

    public async Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(int quizId)
    {
        using var connection = _dbFactory.CreateConnection();
        return await connection.QueryAsync<Question>(
            "SELECT * FROM Questions WHERE QuizId = @QuizId ORDER BY QuestionOrder",
            new { QuizId = quizId });
    }

    public async Task<IEnumerable<Answer>> GetAnswersByQuizIdAsync(int quizId)
    {
        using var connection = _dbFactory.CreateConnection();
        return await connection.QueryAsync<Answer>(
            @"SELECT a.* FROM Answers a
              INNER JOIN Questions q ON a.QuestionId = q.Id
              WHERE q.QuizId = @QuizId
              ORDER BY a.QuestionId, a.AnswerOrder",
            new { QuizId = quizId });
    }

    public async Task<QuizAttempt> CreateAttemptAsync(QuizAttempt attempt)
    {
        using var connection = _dbFactory.CreateConnection();
        var id = await connection.QuerySingleAsync<int>(
            @"INSERT INTO QuizAttempts (UserId, QuizId, Score, TotalPoints, TimeTakenSeconds)
              VALUES (@UserId, @QuizId, @Score, @TotalPoints, @TimeTakenSeconds);
              SELECT LAST_INSERT_ID();",
            new { attempt.UserId, attempt.QuizId, attempt.Score, attempt.TotalPoints, attempt.TimeTakenSeconds });

        attempt.Id = id;
        return attempt;
    }
}
