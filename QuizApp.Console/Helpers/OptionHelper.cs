using QuizAppConsole.Constants;

namespace QuizAppConsole.Helpers;

public static class OptionHelper
{
    public static char ToOptionLetter(int optionId)
    {
        int startID = 0;
        int endID = 25;

        string errorMessage = string.Format(AppConstants.OPTION_RANGE_ERROR_MESSAGE_TEMPLATE, startID, endID);

        if (optionId < startID || optionId > endID)
            throw new ArgumentException(errorMessage);

        return (char)(65 + optionId);
    }

    public static int FromOptionLetter(char optionLetter)
    {
        if (!char.IsLetter(optionLetter))
            throw new ArgumentException(AppConstants.INPUT_MUST_BE_LETTER_ERROR_MESSAGE);

        return char.ToUpper(optionLetter) - 65;
    }
}