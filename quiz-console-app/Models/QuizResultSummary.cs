namespace quiz_console_app.Models;


// ScoringRules buna göre net hesaplaması yapılcaktır.
public class QuizResultSummary
{
    public int CorrectCount { get; set; } = 0;
    public int IncorrectCount { get; set; } = 0;
    public int BlankCount { get; set; } = 0;
    public int NetCount => CorrectCount - (IncorrectCount + BlankCount);
    public double SuccessPercentage => CalculateSuccessPercentage();
    public int TotalQuestions { get; set; } = 0;

    public int QuestionCurrentNumber { get; set; } = 1;

    public QuizResultSummary(int totalQuestions)
    {
        this.TotalQuestions = totalQuestions;
    }

    private double CalculateSuccessPercentage()
    {
        int totalQuestions = CorrectCount + IncorrectCount + BlankCount;
        if (totalQuestions == 0)
        {
            return 0.0;
        }

        return (double)CorrectCount / totalQuestions * 100;
    }
}
