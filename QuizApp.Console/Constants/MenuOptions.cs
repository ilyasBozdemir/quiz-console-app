using QuizAppConsole.Services;
using QuizAppConsole.Views;
using QuizAppConsole.Enums;
using QuizAppConsole.Models;

namespace QuizAppConsole.Constants;

public static class MenuOptions
{
    private static ExportDataView exportDataView = new ExportDataView();

    public static readonly MenuOption[] GeneralOptions =
    {
        new MenuOption(name: AppConstants.QUIZ_MODE_TITLE, action: () => new QuizModeView().StartQuiz()),
        new MenuOption(
            name: AppConstants. DATA_MANIPULATION_MODE_TITLE,
            action: () => exportDataView.DisplayMenuOptions()
        )
    };

    public static readonly MenuOption[] ExportOptions =
    {
        new MenuOption(
            name: string.Format(AppConstants.CREATE_AND_EXPORT_FILE_TITLE_TEMPLATE,AppConstants.JSON_FORMAT_NAME),
            action: () => new ExportService().Export(ExportType.Json)
        ),
        new MenuOption(
            name: string.Format(AppConstants.CREATE_AND_EXPORT_FILE_TITLE_TEMPLATE,AppConstants.XML_FORMAT_NAME),
            action: () => new ExportService().Export(ExportType.Xml)
        ),
        new MenuOption(
            name: AppConstants.RETURN_TO_MAIN_MENU,
            action: () =>
            {
                Console.Clear();
                new QuizMainMenuView().Show();
            }
        )
    };
}
