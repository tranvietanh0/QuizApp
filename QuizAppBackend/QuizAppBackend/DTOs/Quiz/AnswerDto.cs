namespace QuizAppBackend.DTOs.Quiz;

public class AnswerDto
{
    public int Id { get; set; }
    public string AnswerText { get; set; } = string.Empty;
    public int AnswerOrder { get; set; }
}
