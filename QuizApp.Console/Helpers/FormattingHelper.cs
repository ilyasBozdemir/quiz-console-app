namespace QuizAppConsole.Helpers;

public static class FormattingHelper
{
    public static string FormatQuestionNumber(int currentNumber, int totalQuestions)
    {
        int numberOfDigits = totalQuestions.ToString().Length;
        return currentNumber.ToString().PadLeft(numberOfDigits, '0');
    }
}
