using Newtonsoft.Json;
using quiz_console_app.Exceptions;
using quiz_console_app.Models;

namespace quiz_console_app.Helpers;
public class QuestionLoader
{
    public List<Question> LoadQuestionsFromJson(string jsonFilePath)
    {
        try
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFilePath);
            string json = File.ReadAllText(fullPath);
            List<Question> questions = JsonConvert.DeserializeObject<List<Question>>(json);
            return questions;
        }
        catch (JsonLoadFailedException ex)
        {
            throw new JsonLoadFailedException("JSON dosyası yüklenirken bir hata oluştu.", ex);
        }
    }
    public async Task<List<Question>> LoadQuestionsFromUrl(Uri url)
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
                    return LoadQuestionsFromJson(await response.Content.ReadAsStringAsync());
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