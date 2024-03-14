namespace quiz_console_app.ViewModels;

public class BookletViewModel
{
    public int Id { get; set; }
    public string BookletName { get; set; }
    public List<QuestionViewModel> Questions { get; set; }
}
