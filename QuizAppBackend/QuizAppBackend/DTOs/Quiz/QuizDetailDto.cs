namespace QuizAppBackend.DTOs.Quiz;

public class QuizDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public int TimeLimitSeconds { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}
