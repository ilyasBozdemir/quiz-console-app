using quiz_console_app.Models;
using quiz_console_app.Views;

namespace quiz_console_app.Constants;

public static class MenuOptions
{
    public static readonly MenuOption[] GeneralOptions =
    {
        new MenuOption(name: "Quiz Çözme Modu", action: () => new QuizModeView().StartQuiz()),
        new MenuOption(
            name: "Verileri Oluşturma ve Dışa Aktarma Modu",
            action: () => new ExportDataView().DisplayMenuOptions()
        )
    };

    public static readonly MenuOption[] ExportOptions =
    {
        new MenuOption(
            name: "Kitapçık Oluştur ve JSON Olarak Dışa Aktar",
            action: () => new ExportDataView().CreateAndExportBookletToJson()
        ),
        new MenuOption(
            name: "Kitapçık Oluştur ve XML Olarak Dışa Aktar",
            action: () => new ExportDataView().CreateAndExportBookletToXml()
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
