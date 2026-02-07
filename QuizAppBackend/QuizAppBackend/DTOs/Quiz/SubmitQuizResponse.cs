namespace QuizAppBackend.DTOs.Quiz;

public class SubmitQuizResponse
{
    public int Score { get; set; }
    public int TotalPoints { get; set; }
    public int CorrectCount { get; set; }
    public int TotalQuestions { get; set; }
    public List<QuestionResultDto> Results { get; set; } = new();
}

public class QuestionResultDto
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public int SelectedAnswerId { get; set; }
    public int CorrectAnswerId { get; set; }
    public bool IsCorrect { get; set; }
    public int Points { get; set; }
}
