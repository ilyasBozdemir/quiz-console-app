namespace QuizAppConsole.Models;
public class UserQuiz
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; } = Guid.NewGuid();
    public User User { get; set; } = new ();
    public Guid QuizId { get; set; } = Guid.NewGuid();
    public QuizAppConsole.Models.Quiz.Quiz Quiz { get; set; } = new();
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; }
    public bool IsCompleted { get; set; } = false;
}
