using quiz_console_app.Constants;
using quiz_console_app.Services;

namespace quiz_console_app.Views;

public class QuizMainMenuView
{
    public void Show()
    {
        MenuManager menuManager = new MenuManager();
        menuManager.AddMenuOptions(MenuOptions.GeneralOptions);
        menuManager.ExecuteMenu();
    }
}