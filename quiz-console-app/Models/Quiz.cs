using quiz_console_app.Configurations;

namespace quiz_console_app.Models;

public class Quiz
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int DurationInMinutes { get; set; } = 20;
    public bool IsActive { get; set; }
    public bool IsOpenToPublic { get; set; }
    public string Description { get; set; }
    public string Creator { get; set; }

    public List<QuizQuestion> Questions { get; set; }
    public ScoringRules ScoringRules { get; set; }

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
