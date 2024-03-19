namespace quiz_console_app.Helpers;

public static class OptionHelper
{
    public static char ToOptionLetter(int optionId)
    {
        if (optionId < 0 || optionId > 25)
            throw new ArgumentException("Option ID must be between 0 and 25");

        return ((char)(65 + optionId));
    }

    public static int FromOptionLetter(char optionLetter)
    {
        if (!char.IsLetter(optionLetter))
            throw new ArgumentException("Input must be a letter.");

        return char.ToUpper(optionLetter) - 65;
    }
}