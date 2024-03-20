using Newtonsoft.Json;
using quiz_console_app.Enums;
using quiz_console_app.Models;
using quiz_console_app.ViewModels;
using System.Xml.Serialization;

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

        _quizService.GenerateBooklets(bookletCount);
        _userAnswers = new List<UserAnswerKeyViewModel>();
        _answerKeys = QuizService.AnswerKeys;
        Booklets = QuizService.Booklets;

        Console.WriteLine("Dışa aktarma yöntemi seçin:");
        Console.WriteLine("1. Tüm kitapçıkları tek dosyada dışa aktar");
        Console.WriteLine("2. Her kitapçık için ayrı dosyada dışa aktar");
        Console.Write("Seçiminizi yapın (1 veya 2): ");


        int choice;

        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Geçersiz giriş. Lütfen 1 veya 2 girin.");
            return; 
        }

        switch (choice)
        {
            case 1:
                Console.Clear();
                Console.Write("Kitapçık için İsmi girin?: ");
                string bookletName = Console.ReadLine();
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
                break;

            case 2:
                switch (exportType)
                {
                    case ExportType.Json:
                        ExportEachToJson();
                        break;
                    case ExportType.Xml:
                        ExportEachToXml();
                        break;
                    default:
                        Console.WriteLine("Invalid export type.");
                        break;
                }
                break;

            default:
                Console.WriteLine("Geçersiz giriş. Lütfen 1 veya 2 girin.");
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
    public void ExportEachToJson()
    {
        foreach (var booklet in Booklets)
        {
            string fileName = $"{booklet.BookletName}.json";
            string filePath = GetUniqueFilePath(baseDirectory, fileName, "json");

            try
            {
                using (StreamWriter file = File.CreateText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, booklet);
                }

                Console.WriteLine($"Kitapçık '{booklet.BookletName}' JSON olarak başarıyla dışa aktarıldı. Dosya yolu: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON olarak dışa aktarma sırasında bir hata oluştu: {ex.Message}");
            }
        }

        Console.ReadLine();
    }

    public void ExportEachToXml()
    {
        foreach (var booklet in Booklets)
        {
            string fileName = $"{booklet.BookletName}.xml";
            string filePath = GetUniqueFilePath(baseDirectory, fileName, "xml");

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BookletViewModel));
                using (StreamWriter file = new StreamWriter(filePath))
                {
                    serializer.Serialize(file, booklet);
                }

                Console.WriteLine($"Kitapçık '{booklet.BookletName}' XML olarak başarıyla dışa aktarıldı. Dosya yolu: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML olarak dışa aktarma sırasında bir hata oluştu: {ex.Message}");
            }
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
