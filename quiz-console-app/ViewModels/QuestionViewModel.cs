using quiz_console_app.Enums;

namespace quiz_console_app.ViewModels;

public class QuestionViewModel
{
    public int Id { get; set; }
    public string AskText { get; set; }
    public string Explanation { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public List<QuestionOptionViewModel> QuestionOptions { get; set; }
}
