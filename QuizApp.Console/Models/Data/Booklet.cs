namespace QuizAppConsole.Models.Data;

public class Booklet
{
    public int Id { get; set; } = 0;
    public string BookletName { get; set; } = "";
    public List<BookletQuestion> Questions { get; set; } = new List<BookletQuestion>();
}
