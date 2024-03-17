using quiz_console_app.Enums;

namespace quiz_console_app.Models;

public class BookletQuestion
{
    public int Id { get; set; }
    public string AskText { get; set; }
    public string Explanation { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public List<BookletQuestionOption> QuestionOptions { get; set; }
    public BookletQuestion()
    {
        QuestionOptions = new List<BookletQuestionOption>();
    }
}
