using Newtonsoft.Json;
using quiz_console_app.Models;

namespace quiz_console_app.Helpers;

public class QuestionLoader
{
    public List<Question> LoadQuestionsFromJson(string jsonFilePath)
    {
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFilePath);
        string json = File.ReadAllText(fullPath);
        List<Question> questions = JsonConvert.DeserializeObject<List<Question>>(json);
        return questions;
    }
}