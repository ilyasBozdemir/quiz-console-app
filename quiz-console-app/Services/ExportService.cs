using Newtonsoft.Json;
using quiz_console_app.Constants;
using quiz_console_app.Enums;
using quiz_console_app.Models;
using quiz_console_app.ViewModels;
using System.Xml.Serialization;

namespace quiz_console_app.Services;


public class ExportService
{
    public static List<BookletViewModel> Booklets { get; private set; }

    private AnswerKeyCollection _answerKeys;

    private const string baseDirectory = AppConstants.BaseDirectory;

    private QuizService _quizService;

    public void Export(ExportType exportType)
    {
        _quizService = new QuizService();

        Console.Write("Kaç kitapçık dışa aktarılacak?: ");
        int bookletCount = int.Parse(Console.ReadLine());

        _quizService.GenerateBooklets(bookletCount);
        _answerKeys = QuizService.AnswerKeys;
        Booklets = QuizService.Booklets;

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
    }


    public void ExportEachToJson()
    {
        foreach (var booklet in Booklets)
        {
            string bookletFilePath = GetUniqueFilePath(baseDirectory, $"{booklet.BookletName}_Booklet", "json");
            string answerKeysFilePath = GetUniqueFilePath(baseDirectory, $"{booklet.BookletName}_AnswerKeys", "json");

            try
            {
                using (StreamWriter bookletFile = File.CreateText(bookletFilePath))
                using (StreamWriter answerKeysFile = File.CreateText(answerKeysFilePath))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    // Kitapçıkı JSON olarak dışa aktar
                    serializer.Serialize(bookletFile, booklet);
                    Console.WriteLine($"Kitapçık '{booklet.BookletName}' JSON olarak başarıyla dışa aktarıldı. Dosya yolu: {bookletFilePath}");

                    // Cevap anahtarlarını al ve JSON olarak dışa aktar
                    if (_answerKeys.GetAnswerKeys(booklet.Id) is List<AnswerKeyViewModel> answerKeys)
                    {
                        serializer.Serialize(answerKeysFile, answerKeys);
                        Console.WriteLine($"Kitapçık '{booklet.BookletName}' için cevap anahtarları JSON olarak başarıyla dışa aktarıldı. Dosya yolu: {answerKeysFilePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Dışa aktarma sırasında bir hata oluştu: {ex.Message}");
            }
        }

        Console.ReadLine();
    }


    public void ExportEachToXml()
    {
        foreach (var booklet in Booklets)
        {
            string bookletFilePath = GetUniqueFilePath(baseDirectory, $"{booklet.BookletName}_Booklet", "xml");
            string answerKeysFilePath = GetUniqueFilePath(baseDirectory, $"{booklet.BookletName}_AnswerKeys", "xml");

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BookletViewModel));
                using (StreamWriter bookletFile = new StreamWriter(bookletFilePath))
                using (StreamWriter answerKeysFile = new StreamWriter(answerKeysFilePath))
                {
                    // Kitapçığı XML olarak dışa aktar
                    serializer.Serialize(bookletFile, booklet);
                    Console.WriteLine($"Kitapçık '{booklet.BookletName}' XML olarak başarıyla dışa aktarıldı. Dosya yolu: {bookletFilePath}");

                    // Cevap anahtarlarını al ve XML olarak dışa aktar
                    if (_answerKeys.GetAnswerKeys(booklet.Id) is List<AnswerKeyViewModel> answerKeys)
                    {
                        serializer = new XmlSerializer(typeof(List<AnswerKeyViewModel>));
                        serializer.Serialize(answerKeysFile, answerKeys);
                        Console.WriteLine($"Kitapçık '{booklet.BookletName}' için cevap anahtarları XML olarak başarıyla dışa aktarıldı. Dosya yolu: {answerKeysFilePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Dışa aktarma sırasında bir hata oluştu: {ex.Message}");
            }
        }

        Console.ReadLine();
    }


    private string GetUniqueFilePath(string baseDirectory, string baseFileName, string fileExtension)
    {
        if (!Directory.Exists(baseDirectory))
            Directory.CreateDirectory(baseDirectory);

        //string dateTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        // Dosya adı oluştur
        string fileName = $"{baseFileName}.{fileExtension}";
        string filePath = Path.Combine(baseDirectory, fileName);

        int index = 1;

        // Eğer dosya adı zaten varsa, indeksi artırarak farklı bir dosya adı oluştur
        while (File.Exists(filePath))
        {
            fileName = $"{baseFileName}_{index++}.{fileExtension}";
            filePath = Path.Combine(baseDirectory, fileName);
        }

        return filePath;
    }

}