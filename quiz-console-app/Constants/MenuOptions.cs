using quiz_console_app.Models;
using quiz_console_app.Screens;

namespace quiz_console_app.Constants;

public static class MenuOptions
{
    public static readonly MenuOption[] GeneralOptions =
    {
        new MenuOption(1, "Quiz Çözme Modu", () =>  new QuizModeView().StartQuiz()),
        new MenuOption(2, "Verileri Oluşturma ve Dışa Aktarma Modu", () => new ExportDataView().DisplayMenuOptions())
    };

    public static readonly MenuOption[] ExportOptions =
    {
        new MenuOption(1, "Kitapçık Oluştur ve JSON Olarak Dışa Aktar",() => new ExportDataView().CreateAndExportBookletToJson()),
        new MenuOption(2, "Kitapçık Oluştur ve XML Olarak Dışa Aktar", () => new ExportDataView().CreateAndExportBookletToXml()),
        new MenuOption(3, "Kitapçık Oluştur ve CSV Olarak Dışa Aktar", () => new ExportDataView().CreateAndExportBookletToCsv()),
        new MenuOption(4, "Ana Menüye Dön", () => 
        {
            Console.Clear();
            new QuizMainMenuView().Show();
        })
    };
}


