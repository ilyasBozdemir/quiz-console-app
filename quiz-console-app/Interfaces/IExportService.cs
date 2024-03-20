using quiz_console_app.ViewModels;

namespace quiz_console_app.Interfaces;

public interface IExportService
{
    void Export(List<BookletViewModel> Booklets, string filePath);
}