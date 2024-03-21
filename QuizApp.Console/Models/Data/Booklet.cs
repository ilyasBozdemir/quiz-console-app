namespace QuizAppConsole.Models.Data;

public class Booklet
{
    public int Id { get; set; }
    public string BookletName { get; set; }
    public List<BookletQuestion> Questions { get; set; }

    public Booklet()
    {
        Questions = new List<BookletQuestion>();
    }
}
