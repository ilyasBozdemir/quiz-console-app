using quiz_console_app.Constants;
using quiz_console_app.Services;

namespace quiz_console_app.Screens;

public class ExportDataScreen
{
    public void DisplayMenuOptions()
    {
        Console.Clear();
        MenuManager menuManager = new MenuManager();
        menuManager.AddMenuOptions(MenuOptions.ExportOptions);
        menuManager.ExecuteMenu();
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
}
