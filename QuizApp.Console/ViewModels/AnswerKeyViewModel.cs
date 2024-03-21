namespace QuizAppConsole.ViewModels;

public class AnswerKeyViewModel
{
    public int Id { get; set; }
    public int BookletId { get; set; }
    public int QuestionId { get; set; }
    public int CorrectOptionId { get; set; }
    public string CorrectOptionText { get; set; }
}
