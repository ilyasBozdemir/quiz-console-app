using quiz_console_app.Constants;
using quiz_console_app.Models;

namespace quiz_console_app.Screens;

public class QuizMainMenuScreen
{
    public void Show()
    {
        MenuManager menuManager = new MenuManager();
        menuManager.AddMenuOptions(MenuOptions.GeneralOptions);
        menuManager.DisplayMenu();
        menuManager.HandleSelection();
    }
}