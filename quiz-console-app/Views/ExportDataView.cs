using quiz_console_app.Constants;
using quiz_console_app.Services;

namespace quiz_console_app.Views;

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
