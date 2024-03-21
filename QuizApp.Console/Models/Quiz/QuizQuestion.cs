namespace QuizAppConsole.Models.Quiz;

public class QuizQuestion
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<QuizAnswerOption> AnswerOptions { get; set; } // Sorunun seçenekleri
    public int CorrectAnswerId { get; set; } // Doğru cevabın Id'si
}
