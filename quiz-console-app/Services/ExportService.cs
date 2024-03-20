using Newtonsoft.Json;
using quiz_console_app.Enums;
using quiz_console_app.Models;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Services;

public class ExportService 
{
    public static List<BookletViewModel> Booklets { get; private set; }

    private AnswerKeyCollection _answerKeys;
    private List<UserAnswerKeyViewModel> _userAnswers;

    private const string baseDirectory = @"D:\QuizConsoleApp";
    private QuizService _quizService;

 

    public void Export(ExportType exportType)
    {
        _quizService = new QuizService();

        Console.Write("Kaç kitapçık dışa aktarılacak?: ");
        int bookletCount = int.Parse(Console.ReadLine());

        Console.Clear();
        Console.Write("Kitapçık için İsmi girin?: ");
        string bookletName = Console.ReadLine();

        Console.WriteLine("Kitapçıklar oluşturuluyor.");
        _quizService.GenerateBooklets(bookletCount);

        Console.Clear();

        Console.WriteLine("Kitapçıklar oluşturuldu.");

        _userAnswers = new List<UserAnswerKeyViewModel>();
        _answerKeys = QuizService.AnswerKeys;
        Booklets = QuizService.Booklets;

        bookletName = $"{bookletName.Trim()}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

        switch (exportType)
        {
            case ExportType.Json:
                ExportToJson(bookletName);
                break;
            case ExportType.Xml:
                ExportToXml(bookletName);
                break;
            default:
                Console.WriteLine("Invalid export type.");
                break;
        }
    }


    public void ExportToJson(string bookletName)
    {
        string filePath = GetUniqueFilePath(baseDirectory, bookletName, "json");
        try
        {
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, Booklets);
            }

            Console.WriteLine($"Kitapçık JSON olarak başarıyla dışa aktarıldı. Dosya yolu: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON olarak dışa aktarma sırasında bir hata oluştu: {ex.Message}");
        }

        Console.ReadLine();
    }

    public void ExportToXml(string bookletName)
    {
  
        string filePath = GetUniqueFilePath(baseDirectory, bookletName, "xml");
        try
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<BookletViewModel>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, Booklets);
            }
            Console.WriteLine($"Kitapçık XML olarak başarıyla dışa aktarıldı. Dosya yolu: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"XML olarak dışa aktarma sırasında bir hata oluştu: {ex.Message}");
        }
        Console.ReadLine();
    }
 

    private string GetUniqueFilePath(string baseDirectory, string baseFileName, string fileExtension)
    {
        if(!Directory.Exists(baseDirectory))
            Directory.CreateDirectory(baseDirectory);
        

        string fileName = baseFileName + "." + fileExtension;
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
