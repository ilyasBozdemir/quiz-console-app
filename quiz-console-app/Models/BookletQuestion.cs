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

   
    public bool ValidateCorrectOptionCount()
    {
        int correctCount = QuestionOptions.Count(option => option.IsCorrect);
        return correctCount == 1;
    }

    public bool ValidateIncorrectOptionCount()
    {
        int correctCount = QuestionOptions.Count(option => option.IsCorrect);
        int incorrectCount = QuestionOptions.Count(option => !option.IsCorrect);

        return ValidateIncorrectOptionCount(correctCount, incorrectCount);
    }

    private bool ValidateIncorrectOptionCount(int correctCount, int incorrectCount)
    {
        return incorrectCount == QuestionOptions.Count - 1 && (correctCount + incorrectCount == QuestionOptions.Count);
    }

    private bool ValidateCorrectOptionCount(int correctCount, int incorrectCount)
    {
        return correctCount == 1 && (correctCount + incorrectCount == QuestionOptions.Count);
    }


}
