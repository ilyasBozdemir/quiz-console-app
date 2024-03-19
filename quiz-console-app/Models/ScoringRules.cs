namespace quiz_console_app.Models;

public class ScoringRules
{
    public int CorrectAnswerScore { get; set; } = 1; // Doğru cevap için puan
    public double IncorrectAnswerScore => 1.0 / (NumberOfChoices - 1);  // Yanlış cevap için puanlar
    public int BlankAnswerScore { get; set; } = 0; // Boş cevaplar için puan
    public int NumberOfChoices { get; set; } // Sorunun şık sayısı
    public bool PenaltyForIncorrectAnswer { get; set; } = true; // Yanlış cevaplar için varsayılan olarak ceza uygulanacak
    public ScoringRules(int numberOfChoices)
    {
        NumberOfChoices = numberOfChoices;
    }
}

