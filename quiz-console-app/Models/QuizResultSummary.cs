namespace quiz_console_app.Models;

public class QuizResultSummary
{
    public int CorrectCount { get; set; } = 0;
    public int IncorrectCount { get; set; } = 0;
    public int BlankCount { get; set; } = 0;
    public double NetCount => CalculateNetCount();
    public int TotalQuestions { get; set; } = 0;
    public int QuestionCurrentNumber { get; set; } = 1;
    public ScoringRules ScoringRules { get; set; }
    public QuizResultSummary(int totalQuestions, ScoringRules scoringRules)
    {
        TotalQuestions = totalQuestions;
        this.ScoringRules = scoringRules;
    }

    private double CalculateNetCount()
    {
        return (ScoringRules.PenaltyForIncorrectAnswer)
            ? CorrectCount - ScoringRules.IncorrectAnswerScore * IncorrectCount
            : CorrectCount;
    }
}
