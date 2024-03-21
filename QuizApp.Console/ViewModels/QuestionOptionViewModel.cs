namespace QuizAppConsole.ViewModels;
public class QuestionOptionViewModel
{
    public int Id { get; set; } = 0;
    public string Text { get; set; } = "";
    public bool IsCorrect { get; set; } = false;
}
