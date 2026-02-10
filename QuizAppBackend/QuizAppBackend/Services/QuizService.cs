using QuizAppBackend.Data.Repositories.Interfaces;
using QuizAppBackend.DTOs.Quiz;
using QuizAppBackend.Exceptions;
using QuizAppBackend.Models;
using QuizAppBackend.Services.Interfaces;

namespace QuizAppBackend.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;

    public QuizService(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public async Task<IEnumerable<QuizListItemDto>> GetAllQuizzesAsync()
    {
        return await _quizRepository.GetAllActiveAsync();
    }

    public async Task<QuizDetailDto> GetQuizDetailAsync(int quizId)
    {
        var quiz = await _quizRepository.GetByIdAsync(quizId)
            ?? throw new NotFoundException($"Quiz with ID {quizId} not found.");

        var questions = await _quizRepository.GetQuestionsByQuizIdAsync(quizId);
        var answers = await _quizRepository.GetAnswersByQuizIdAsync(quizId);

        var answersByQuestion = answers.GroupBy(a => a.QuestionId)
            .ToDictionary(g => g.Key, g => g.ToList());

        return new QuizDetailDto
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            Category = quiz.Category,
            TimeLimitSeconds = quiz.TimeLimitSeconds,
            Questions = questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                QuestionOrder = q.QuestionOrder,
                Points = q.Points,
                Answers = answersByQuestion.GetValueOrDefault(q.Id, new List<Answer>())
                    .Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        AnswerText = a.AnswerText,
                        AnswerOrder = a.AnswerOrder
                        // NOTE: IsCorrect is NOT included - anti-cheat
                    }).ToList()
            }).ToList()
        };
    }

    public async Task<SubmitQuizResponse> SubmitQuizAsync(int quizId, int userId, SubmitQuizRequest request)
    {
        var quiz = await _quizRepository.GetByIdAsync(quizId)
            ?? throw new NotFoundException($"Quiz with ID {quizId} not found.");

        var questions = (await _quizRepository.GetQuestionsByQuizIdAsync(quizId)).ToList();
        var answers = (await _quizRepository.GetAnswersByQuizIdAsync(quizId)).ToList();

        var answersByQuestion = answers.GroupBy(a => a.QuestionId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var submittedAnswers = request.Answers.ToDictionary(a => a.QuestionId, a => a.SelectedAnswerId);

        var results = new List<QuestionResultDto>();
        int totalScore = 0;
        int totalPoints = 0;
        int correctCount = 0;

        foreach (var question in questions)
        {
            totalPoints += question.Points;

            var questionAnswers = answersByQuestion.GetValueOrDefault(question.Id, new List<Answer>());
            var correctAnswer = questionAnswers.FirstOrDefault(a => a.IsCorrect);
            var selectedAnswerId = submittedAnswers.GetValueOrDefault(question.Id, 0);

            bool isCorrect = correctAnswer != null && selectedAnswerId == correctAnswer.Id;
            if (isCorrect)
            {
                totalScore += question.Points;
                correctCount++;
            }

            results.Add(new QuestionResultDto
            {
                QuestionId = question.Id,
                QuestionText = question.QuestionText,
                SelectedAnswerId = selectedAnswerId,
                CorrectAnswerId = correctAnswer?.Id ?? 0,
                IsCorrect = isCorrect,
                Points = isCorrect ? question.Points : 0
            });
        }

        var attempt = new QuizAttempt
        {
            UserId = userId,
            QuizId = quizId,
            Score = totalScore,
            TotalPoints = totalPoints
        };

        await _quizRepository.CreateAttemptAsync(attempt);

        return new SubmitQuizResponse
        {
            Score = totalScore,
            TotalPoints = totalPoints,
            CorrectCount = correctCount,
            TotalQuestions = questions.Count,
            Results = results
        };
    }
}
