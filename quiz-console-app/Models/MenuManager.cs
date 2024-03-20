using quiz_console_app.Helpers;

namespace quiz_console_app.Models;

public class MenuManager
{
    private readonly Dictionary<int, MenuOption> menuOptions;
    public string ErrorMessage { get; set; }

    public MenuManager()
    {
        menuOptions = new Dictionary<int, MenuOption>();
        ErrorMessage = "Geçersiz seçim. Lütfen geçerli bir seçenek girin.";
    }

    public void AddMenuOption(MenuOption option) => menuOptions[option.Id] = option;
    

    public void AddMenuOptions(MenuOption[] options)
    {
        foreach (MenuOption option in options)
            AddMenuOption(option);
    }

    public void ExecuteMenu()
    {
        DisplayMenu();
        HandleSelection();
    }


    private void DisplayMenu()
    {
        for (int i = 0; i < menuOptions.Count; i++) 
        {
            ErrorMessage += (i != 0)
               ? (i != menuOptions.Count - 1)
                   ? $", {i + 1}"
                   : $" veya {i + 1}"
               : $"{i + 1}";
        }

        ErrorMessage = $"Geçersiz seçim. Lütfen {ErrorMessage} girin.";

        ConsoleHelper.WriteColoredLine("Seçiminizi yapın\n".ToUpper(), ConsoleColors.Title);

        foreach (var option in menuOptions.Values)
        {
            ConsoleHelper.WriteColored($"{option.Id}", ConsoleColors.Info);
            Console.WriteLine($" => {option.Name}");
        }
    }

    private void HandleSelection()
    {
        bool menuState = true;

        while (menuState)
        {
            Console.ForegroundColor = ConsoleColors.Prompt;
            Console.Write("Seçiminizi yapın: ");
            Console.ForegroundColor = ConsoleColors.Default;
            string userInput = Console.ReadLine();
            int choice;

            if (int.TryParse(userInput, out choice))
                if (menuOptions.ContainsKey(choice))
                    menuOptions[choice].Action();
                else
                    ConsoleHelper.WriteColoredLine("Geçersiz seçim. Lütfen listedeki bir seçeneği seçin.", ConsoleColors.Error);
            else
                ConsoleHelper.WriteColoredLine("Geçersiz giriş. Lütfen bir sayı girin.", ConsoleColors.Error);
        }


        ConsoleHelper.WriteColored("Çıkış için enter tuşuna basın.", ConsoleColors.Debug);
        Console.ReadLine();
    }

}

