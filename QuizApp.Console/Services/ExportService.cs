using Newtonsoft.Json;
using QuizAppConsole.Constants;
using QuizAppConsole.Enums;
using QuizAppConsole.Models;
using QuizAppConsole.ViewModels;
using System.Xml.Serialization;

namespace QuizAppConsole.Services;

public class ExportService
{
    public static List<BookletViewModel> Booklets { get; private set; } = new List<BookletViewModel>();
    private AnswerKeyCollection _answerKeys { get; set; } = new AnswerKeyCollection();
    private const string BaseDirectory = AppConstants.BASE_DIRECTORY;
    private QuizService _quizService { get; set; } = new QuizService();
 
    public void Export(ExportType exportType)
    {
        Console.Write(AppConstants.EXPORT_BOOKLET_QUESTION);

        string input = Console.ReadLine() ?? "";
        int bookletCount;
        if (!int.TryParse(input, out bookletCount))
            bookletCount = 0;

        _quizService.GenerateBooklets(bookletCount);
        _answerKeys = QuizService.AnswerKeys;
        Booklets = QuizService.Booklets;


        switch (exportType)
        {
            case ExportType.JSON:
                ExportEachToJson();
                break;
            case ExportType.XML:
                ExportEachToXml();
                break;
            default:
                Console.WriteLine(AppConstants.INVALID_EXPORT_TYPE_ERROR_MESSAGE);
                break;
        }
    }


    public void ExportEachToJson()
    {
        foreach (var booklet in Booklets)
        {
            string bookletFilePath = GetUniqueFilePath(BaseDirectory, $"{booklet.BookletName}_Booklet", AppConstants.JSON_FORMAT_NAME.ToLower());
            string answerKeysFilePath = GetUniqueFilePath(BaseDirectory, $"{booklet.BookletName}_AnswerKeys", AppConstants.JSON_FORMAT_NAME.ToLower());

            try
            {
                using (StreamWriter bookletFile = File.CreateText(bookletFilePath))
                using (StreamWriter answerKeysFile = File.CreateText(answerKeysFilePath))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    serializer.Serialize(bookletFile, booklet);
                    var successMessage = string.Format(AppConstants.BOOKLET_EXPORT_SUCCESS_MESSAGE_TEMPLATE, booklet.BookletName, AppConstants.JSON_FORMAT_NAME, bookletFilePath);
                    Console.WriteLine(successMessage);

                    if (_answerKeys.GetAnswerKeys(booklet.Id) is List<AnswerKeyViewModel> answerKeys)
                    {
                        serializer.Serialize(answerKeysFile, answerKeys);
                        successMessage = string.Format(AppConstants.ANSWER_KEYS_EXPORT_SUCCESS_MESSAGE_TEMPLATE, booklet.BookletName, AppConstants.JSON_FORMAT_NAME, answerKeysFilePath);
                        Console.WriteLine(successMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(AppConstants.EXPORT_ERROR_MESSAGE_TEMPLATE, ex.Message);
            }
        }

        Console.ReadLine();
    }


    public void ExportEachToXml()
    {
        foreach (var booklet in Booklets)
        {
            string bookletFilePath = GetUniqueFilePath(BaseDirectory, $"{booklet.BookletName}_Booklet", AppConstants.XML_FORMAT_NAME.ToLower());
            string answerKeysFilePath = GetUniqueFilePath(BaseDirectory, $"{booklet.BookletName}_AnswerKeys", AppConstants.XML_FORMAT_NAME.ToLower());

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BookletViewModel));
                using (StreamWriter bookletFile = new StreamWriter(bookletFilePath))
                using (StreamWriter answerKeysFile = new StreamWriter(answerKeysFilePath))
                {
                    serializer.Serialize(bookletFile, booklet);
                    var successMessage = string.Format(AppConstants.BOOKLET_EXPORT_SUCCESS_MESSAGE_TEMPLATE, booklet.BookletName, AppConstants.XML_FORMAT_NAME, bookletFilePath);
                    Console.WriteLine(successMessage);

                    if (_answerKeys.GetAnswerKeys(booklet.Id) is List<AnswerKeyViewModel> answerKeys)
                    {
                        serializer = new XmlSerializer(typeof(List<AnswerKeyViewModel>));
                        serializer.Serialize(answerKeysFile, answerKeys);

                        successMessage = string.Format(AppConstants.ANSWER_KEYS_EXPORT_SUCCESS_MESSAGE_TEMPLATE, booklet.BookletName, AppConstants.XML_FORMAT_NAME, answerKeysFilePath);
                        Console.WriteLine(successMessage);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(AppConstants.EXPORT_ERROR_MESSAGE_TEMPLATE, ex.Message));
            }
        }

        Console.ReadLine();
    }


    private string GetUniqueFilePath(string baseDirectory, string baseFileName, string fileExtension)
    {
        if (!Directory.Exists(baseDirectory))
            Directory.CreateDirectory(baseDirectory);

        //string dateTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        string fileName = $"{baseFileName}.{fileExtension}";
        string filePath = Path.Combine(baseDirectory, fileName);

        int index = 1;

        while (File.Exists(filePath))
        {
            fileName = $"{baseFileName}_{index++}.{fileExtension}";
            filePath = Path.Combine(baseDirectory, fileName);
        }

        return filePath;
    }

}