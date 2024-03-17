using Newtonsoft.Json;
using quiz_console_app.Exceptions;
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
            {
                json = jsonSource;
            }
            
        
            List<BookletQuestion> questions = JsonConvert.DeserializeObject<List<BookletQuestion>>(json);
            return questions;
        }
        catch (JsonLoadFailedException ex)
        {
            throw new JsonLoadFailedException("JSON dosyası yüklenirken bir hata oluştu.", ex);
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
            throw new Exception("Hata oluştu: " + ex.Message);
        }
    }

}