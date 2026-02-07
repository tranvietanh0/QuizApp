using System.ComponentModel.DataAnnotations;

namespace QuizAppBackend.DTOs.Quiz;

public class SubmitQuizRequest
{
    [Required]
    public List<SubmitAnswerItem> Answers { get; set; } = new();
}

public class SubmitAnswerItem
{
    public int QuestionId { get; set; }
    public int SelectedAnswerId { get; set; }
}
