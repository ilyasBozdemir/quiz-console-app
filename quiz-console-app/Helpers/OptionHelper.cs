namespace quiz_console_app.Helpers;

public static class OptionHelper
{
    public static char ToOptionLetter(int optionId)
    {
        int startID = 0;
        int endID = 25;  

        if (optionId < startID || optionId > endID)
            throw new ArgumentException($"Seçenek kimliği {startID} ile {endID} arasında olmalıdır");

        return ((char)(65 + optionId));
    }

    public static int FromOptionLetter(char optionLetter)
    {
        if (!char.IsLetter(optionLetter))
            throw new ArgumentException("Giriş bir harf olmalıdır.");

        return char.ToUpper(optionLetter) - 65;
    }
}