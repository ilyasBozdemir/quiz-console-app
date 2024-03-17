using quiz_console_app.Enums;
using quiz_console_app.Helpers;
using quiz_console_app.Screens;

namespace quiz_console_app.Services;

public class QuizModeHandlerService
{
    public void StartMode(QuizMode mode)
    {
        Console.Clear();
        switch (mode)
        {
            case QuizMode.Quiz:
               new QuizModeScreen().StartQuiz();
                break;
            case QuizMode.DisplayBooklets:
                new DisplayBookletScreen().Start();
                break;
            case QuizMode.ExportData:
                new ExportDataScreen().Start();
                break;
            default:
                Console.WriteLine("Geçersiz mod.");
                break;
        }
    }

    public void ShowMainMenu()
    {

        string errorMessage = "";
        string[] options =
        {
            "Quiz Çözme Modu",
            "Kitapçıkları ve Cevap Anahtarlarını Görüntüleme Modu",
            "Verileri Dışa Aktarma Modu",
        };

        for (int i = 0; i < options.Length; i++)
            errorMessage += (i != 0)
                ? (i != options.Length - 1)
                  ? $", {i + 1}"
                  : $" veya {i + 1}"
                : $"{i + 1}";

        errorMessage = $"Geçersiz seçim. Lütfen {errorMessage} girin.";

        ConsoleHelper.WriteColoredLine("Seçiminizi yapın\n".ToUpper(), ConsoleColors.Title);

        for (int i = 0; i < options.Length; i++)
        {
            ConsoleHelper.WriteColored($"{i + 1}", ConsoleColors.Info);
            Console.WriteLine($" => {options[i]}");
        }
        bool menuState = true;
        while (menuState)
        {
            Console.ForegroundColor = ConsoleColors.Prompt;
            string userInput = Console.ReadLine();
            Console.ResetColor();
            int choice;
            if (int.TryParse(userInput, out choice))
                if (Enum.IsDefined(typeof(QuizMode), choice))
                {
                    StartMode((QuizMode)choice);
                    break;
                }
                else
                    ConsoleHelper.WriteColoredLine(errorMessage, ConsoleColors.Error);

            else
                ConsoleHelper.WriteColoredLine("Geçersiz giriş. Lütfen bir sayı girin.", ConsoleColors.Error);
        }

        ConsoleHelper.WriteColored("Çıkış için enter tuşuna basın.", ConsoleColors.Debug);
       
        Console.ReadLine();
    }

}