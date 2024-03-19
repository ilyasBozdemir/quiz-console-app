namespace quiz_console_app.ViewModels;

public class UserAnswerKeyViewModel 
{
    public int Id { get; set; }
    public int BookletId { get; set; }
    public int QuestionId { get; set; }
    public string UserAnswerOption { get; set; }
    public string UserAnswerOptionText { get; set; }
    public int UserAnswerOptionId { get; set; }
}
