using QuizAppBackend.DTOs.Quiz;

namespace QuizAppBackend.Services.Interfaces;

public interface IQuizService
{
    Task<IEnumerable<QuizListItemDto>> GetAllQuizzesAsync();
    Task<QuizDetailDto> GetQuizDetailAsync(int quizId);
    Task<SubmitQuizResponse> SubmitQuizAsync(int quizId, int userId, SubmitQuizRequest request);
}
