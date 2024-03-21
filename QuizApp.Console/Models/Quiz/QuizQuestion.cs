namespace QuizAppConsole.Models.Quiz;

public class QuizQuestion
{
    public int Id { get; set; } = 0;
    public string Text { get; set; } = "";
    public List<QuizAnswerOption> AnswerOptions { get; set; } = new List<QuizAnswerOption>(); // Sorunun seçenekleri
    public int CorrectAnswerId { get; set; } = 0; // Doğru cevabın Id'si
}
