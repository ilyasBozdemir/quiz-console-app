using quiz_console_app.Models;

namespace quiz_console_app.Validators;

public static class QuestionValidator
{
    public static bool ValidateQuestionOptions(List<BookletQuestionOption> options)
    {
        int correctCount = options.Count(option => option.IsCorrect);
        return correctCount == 1 && correctCount + 1 == options.Count;
    }
}
