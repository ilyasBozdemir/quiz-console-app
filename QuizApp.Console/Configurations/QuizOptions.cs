using QuizAppConsole.Models.Quiz;

namespace QuizAppConsole.Configurations;

public class QuizOptions
{
    public int DurationInMinutes { get; set; } = 45;
    public bool IsOpenToPublic { get; set; } = true;
    public int NumberOfChoices { get; set; }
    public ScoringRules ScoringRules { get; set; }
    public List<QuizQuestion> Questions { get; set; }
}

