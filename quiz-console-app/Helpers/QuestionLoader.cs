using Newtonsoft.Json;
using quiz_console_app.Models;

namespace quiz_console_app.Helpers;
public class QuestionLoader
{
    public List<BookletQuestion> LoadQuestionsFromJson(string jsonFilePath = null, string jsonSource = null)
    {
        try
        {
            string json = null, fullPath = null;

            if (jsonFilePath != null)
            {
                fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFilePath);
                json = File.ReadAllText(fullPath);
            }
            if (jsonSource != null)
                json = jsonSource;
            
            if (json == null)
                throw new ArgumentNullException("JSON dosyası yolu veya içeriği belirtilmemiş.");
            

            List<BookletQuestion> questions = JsonConvert.DeserializeObject<List<BookletQuestion>>(json);

            foreach (var question in questions)
            {
                if (!question.ValidateCorrectOptionCount())
                    ConsoleHelper.WriteColoredLine($"{question.Id}. Soru için doğru şık sayısı geçerli değil. Sadece bir tane doğru şık olmalıdır.", ConsoleColors.Error);
                
                if (!question.ValidateIncorrectOptionCount())
                    ConsoleHelper.WriteColoredLine($"{question.Id}. Soru için yanlış şık sayısı geçerli değil. Yanlış şık olmamalıdır.", ConsoleColors.Error);
            }



            return questions;
        }
        catch (JsonReaderException ex)
        {
            ConsoleHelper.WriteColored($"JSON formatında bir hata oluştu: {ex.Message}", ConsoleColors.Error);
            return new List<BookletQuestion>();
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteColored($"JSON dosyası yüklenirken bir hata oluştu: {ex.Message}", ConsoleColors.Error);
            return new List<BookletQuestion>();
        }

    }


    public async Task<List<BookletQuestion>> LoadQuestionsFromUrl(Uri url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string contentType = response.Content.Headers.ContentType.MediaType;

                if (contentType != "application/json")
                    throw new InvalidOperationException($"Beklenen JSON içeriği değil., alınan medya türü: {contentType}");


                if (response.IsSuccessStatusCode)
                    return LoadQuestionsFromJson(jsonFilePath: null, jsonSource: await response.Content.ReadAsStringAsync());
                else
                    throw new Exception("API'den veri alınamadı. Hata kodu: " + response.StatusCode);

            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteColoredLine($"Hata oluştu: {ex.Message}", ConsoleColors.Error);
            return new List<BookletQuestion>();
        }
    }

}