using quiz_console_app.Enums;
using quiz_console_app.Models;
using quiz_console_app.Services;
using quiz_console_app.Views;

namespace quiz_console_app.Constants;

public static class MenuOptions
{
    private static ExportDataView exportDataView = new ExportDataView();

    public static readonly MenuOption[] GeneralOptions =
    {
        new MenuOption(name: "Quiz Çözme Modu", action: () => new QuizModeView().StartQuiz()),
        new MenuOption(
            name: "Verileri Oluşturma ve Dışa Aktarma Modu",
            action: () => exportDataView.DisplayMenuOptions()
        )
    };

    public static readonly MenuOption[] ExportOptions =
    {
        new MenuOption(
            name: "Kitapçık Oluştur ve JSON Olarak Dışa Aktar",
            action: () => new ExportService().Export(ExportType.Json)
        ),
        new MenuOption(
            name: "Kitapçık Oluştur ve XML Olarak Dışa Aktar",
            action: () => new ExportService().Export(ExportType.Xml)
        ),
        new MenuOption(
            name: "Ana Menüye Dön",
            action: () =>
            {
                Console.Clear();
                new QuizMainMenuView().Show();
            }
        )
    };
}
