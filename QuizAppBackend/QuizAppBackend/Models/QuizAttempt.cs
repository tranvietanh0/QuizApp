namespace QuizAppBackend.Models;

public class QuizAttempt
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuizId { get; set; }
    public int Score { get; set; }
    public int TotalPoints { get; set; }
    public int? TimeTakenSeconds { get; set; }
    public DateTime CompletedAt { get; set; }
}
