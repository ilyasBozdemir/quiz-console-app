namespace quiz_console_app.Models;

public class QuizQuestion
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<AnswerOption> AnswerOptions { get; set; } // Sorunun seçenekleri
    public int CorrectAnswerId { get; set; } // Doğru cevabın Id'si
}
