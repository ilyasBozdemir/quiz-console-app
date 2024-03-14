using quiz_console_app.Enums;

namespace quiz_console_app.Models;

public class Question
{
    public int Id { get; set; }
    public string AskText { get; set; }
    public string Explanation { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public List<QuestionOption> QuestionOptions { get; set; }
    public Question()
    {
        QuestionOptions = new List<QuestionOption>();
    }
}
