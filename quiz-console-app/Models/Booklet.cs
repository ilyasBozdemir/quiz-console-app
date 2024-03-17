using quiz_console_app.Enums;

namespace quiz_console_app.Models;

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
