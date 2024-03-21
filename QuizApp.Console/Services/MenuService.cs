using QuizAppConsole.Constants;
using QuizAppConsole.Helpers;
using QuizAppConsole.Models;

namespace QuizAppConsole.Services;

public class MenuService
{
    private readonly Dictionary<int, MenuOption> menuOptions;
    public string ErrorMessage { get; set; }

    public MenuService()
    {
        menuOptions = new Dictionary<int, MenuOption>();
        ErrorMessage = AppConstants.INVALID_SELECTION_ERROR_MESSAGE;
    }

    public void AddMenuOption(MenuOption option) => menuOptions[option.Id] = option;


    public void AddMenuOptions(MenuOption[] options)
    {
        int idCounter = menuOptions.Keys.Count > 0 ? menuOptions.Keys.Max() + 1 : 1;

        foreach (MenuOption option in options)
        {
            option.Id = idCounter++;
            AddMenuOption(option);
        }
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
            ErrorMessage += i != 0
               ? i != menuOptions.Count - 1
                   ? $", {i + 1}"
                   : $" veya {i + 1}"
               : $"{i + 1}";
        }

        ErrorMessage = string.Format(AppConstants.INVALID_SELECTION_ERROR_MESSAGE_TEMPLATE, ErrorMessage);


        ConsoleHelper.WriteColoredLine(AppConstants.CHOOSE_SELECTION_PROMPT.ToUpper(), ConsoleColors.Title);

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
            Console.Write(AppConstants.CHOOSE_SELECTION_PROMPT);
            Console.ForegroundColor = ConsoleColors.Default;
            string userInput = Console.ReadLine();
            int choice;

            if (int.TryParse(userInput, out choice))
                if (menuOptions.ContainsKey(choice))
                {
                    menuOptions[choice].Action();
                    break;
                }
                else
                    ConsoleHelper.WriteColoredLine(AppConstants.INVALID_SELECTION_MESSAGE, ConsoleColors.Error);
            else
                ConsoleHelper.WriteColoredLine(AppConstants.INVALID_INPUT_MESSAGE, ConsoleColors.Error);
        }

        ConsoleHelper.WriteColored(AppConstants.EXIT_INSTRUCTION, ConsoleColors.Debug);
        Console.ReadLine();
    }

}

