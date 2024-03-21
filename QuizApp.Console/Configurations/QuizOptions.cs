using QuizAppConsole.Models.Quiz;

namespace QuizAppConsole.Configurations;

public class QuizOptions
{
    public int DurationInMinutes { get; set; } = 45;
    public bool IsOpenToPublic { get; set; } = true;
    public int NumberOfChoices { get; set; } = 5;
    public ScoringRules ScoringRules { get; set; } = new ScoringRules(0);
    public List<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
}

