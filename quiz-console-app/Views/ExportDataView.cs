using quiz_console_app.Constants;
using quiz_console_app.Interfaces;
using quiz_console_app.Services;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Views;

public class ExportDataView
{
    private List<BookletViewModel> Booklets { get; set; } = QuizService.Booklets;
    private string FilePath { get; set; }

    public void DisplayMenuOptions()
    {
        Console.Clear();
        MenuManager menuManager = new MenuManager();
        menuManager.AddMenuOptions(MenuOptions.ExportOptions);
        menuManager.ExecuteMenu();
    }

    public void CreateAndExportBookletToJson()
    {
        IExportService exportService = new JsonExportService();
        exportService.Export(Booklets, FilePath);
        Console.WriteLine("Kitapçık JSON olarak başarıyla dışa aktarıldı.");
    }
    public void CreateAndExportBookletToXml()
    {
        IExportService exportService = new XmlExportService();
        exportService.Export(Booklets, FilePath);
        Console.WriteLine("Kitapçık XML olarak başarıyla dışa aktarıldı.");
    }

}
