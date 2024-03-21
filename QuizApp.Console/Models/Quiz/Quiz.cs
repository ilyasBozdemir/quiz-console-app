using QuizAppConsole.Configurations;

namespace QuizAppConsole.Models.Quiz;

public class Quiz
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "";
    public DateTime StartTime { get; set; } = DateTime.Now;
    public int DurationInMinutes { get; set; } = 20;
    public DateTime EndTime { get; set; } = DateTime.Now;

    public bool IsActive { get; set; } = false;
    public bool IsOpenToPublic { get; set; } = false;
    public string Description { get; set; } = "";
    public string Creator { get; set; } = "";

    public List<QuizQuestion> Questions { get; set; }
    public ScoringRules ScoringRules { get; set; }

    public Quiz()
    {
        
    }
    public Quiz(Action<QuizOptions> options)
    {
        QuizOptions quizOptions = new QuizOptions();
        options?.Invoke(quizOptions);

        DurationInMinutes = quizOptions.DurationInMinutes;
        IsOpenToPublic = quizOptions.IsOpenToPublic;

        ScoringRules = new ScoringRules(quizOptions.NumberOfChoices);

        StartTime = DateTime.Now;
        EndTime = StartTime.AddMinutes(DurationInMinutes);
        IsActive = DateTime.Now >= StartTime && DateTime.Now <= EndTime;

    }
    public void ConfigureQuiz(Action<QuizOptions> configure)
    {
        var options = new QuizOptions();
        configure(options);
        ApplyOptions(options);
    }

    private void ApplyOptions(QuizOptions options)
    {
        DurationInMinutes = options.DurationInMinutes;
        IsOpenToPublic = options.IsOpenToPublic;
    }

}
