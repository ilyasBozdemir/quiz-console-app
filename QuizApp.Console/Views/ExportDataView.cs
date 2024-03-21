using QuizAppConsole.Constants;
using QuizAppConsole.Services;

namespace QuizAppConsole.Views;

public class ExportDataView
{
    public void DisplayMenuOptions()
    {
        Console.Clear();
        MenuService menuManager = new MenuService();
        menuManager.AddMenuOptions(MenuOptions.ExportOptions);
        menuManager.ExecuteMenu();
    }
}
