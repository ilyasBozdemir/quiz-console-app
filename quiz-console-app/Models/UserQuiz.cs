namespace quiz_console_app.Models;

public class UserQuiz
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool IsCompleted { get; set; } = false;
}
