using System.Xml.Serialization;

namespace QuizAppConsole.ViewModels;

public class BookletViewModel
{
    public int Id { get; set; } = 0;
    public string BookletName { get; set; } = "";
    public string Prefix { get; set; } = "";
    public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
}