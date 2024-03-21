using QuizAppConsole.Constants;
using QuizAppConsole.Services;

namespace QuizAppConsole.Views;

public class QuizMainMenuView
{
    public void Show()
    {
        MenuService menuManager = new MenuService();
        menuManager.AddMenuOptions(MenuOptions.GeneralOptions);
        menuManager.ExecuteMenu();
    }
}