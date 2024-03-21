namespace QuizAppConsole.ViewModels;

public class AnswerKeyViewModel
{
    public int Id { get; set; } = 0;
    public int BookletId { get; set; } = 0;
    public int QuestionId { get; set; } = 0;
    public int CorrectOptionId { get; set; } = 0;
    public string CorrectOptionText { get; set; } = "";
}
