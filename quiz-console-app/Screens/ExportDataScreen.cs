using quiz_console_app.Helpers;
using quiz_console_app.Services;

namespace quiz_console_app.Screens;

public class ExportDataScreen
{
    public void Start()
    {
        ShowMenu();
    }

    public void ShowMenu()
    {
        string errorMessage = "";
        string[] options =
        {
           "Kitapçık Oluştur ve JSON Olarak Dışa Aktar",
           "Kitapçık Oluştur ve XML Olarak Dışa Aktar",
           "Kitapçık Oluştur ve CSV Olarak Dışa Aktar",
           "Ana Menüye Dön"
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
            Console.Write("Seçiminizi yapın: ");
            Console.ForegroundColor = ConsoleColors.Default;
            string userInput = Console.ReadLine();
            Console.ResetColor();
            int choice;

            if (int.TryParse(userInput, out choice))
                HandleMenuOption(choice);
            else
                ConsoleHelper.WriteColoredLine("Geçersiz giriş. Lütfen bir sayı girin.", ConsoleColors.Error);

        }
    }

    private void HandleMenuOption(int option)
    {
        switch (option)
        {
            case 1:
                CreateAndExportBookletToJson();
                break;
            case 2:
                CreateAndExportBookletToXml();
                break;
            case 3:
                CreateAndExportBookletToCsv();
                break;
            case 4:
                ReturnToMainMenu();
                break;
            default:
                Console.WriteLine("Geçersiz seçenek. Lütfen geçerli bir seçenek seçin.");
                break;
        }
    }


    public void CreateAndExportBookletToJson()
    {
        Console.WriteLine("CreateAndExportBookletToJson");
    }

    public void CreateAndExportBookletToXml()
    {
        Console.WriteLine("CreateAndExportBookletToXml");
    }

    public void CreateAndExportBookletToCsv()
    {
        Console.WriteLine("CreateAndExportBookletToCsv");
    }

    public void ReturnToMainMenu()
    {
        Console.Clear();
        new QuizModeHandlerService().ShowMainMenu();
    }

}
