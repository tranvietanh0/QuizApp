using QuizAppBackend.DTOs.Quiz;
using QuizAppBackend.Models;

namespace QuizAppBackend.Data.Repositories.Interfaces;

public interface IQuizRepository
{
    Task<IEnumerable<QuizListItemDto>> GetAllActiveAsync();
    Task<Quiz?> GetByIdAsync(int id);
    Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(int quizId);
    Task<IEnumerable<Answer>> GetAnswersByQuizIdAsync(int quizId);
    Task<QuizAttempt> CreateAttemptAsync(QuizAttempt attempt);
}
