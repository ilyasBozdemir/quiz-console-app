using Newtonsoft.Json;
using QuizAppConsole.Constants;
using QuizAppConsole.Helpers;
using QuizAppConsole.Models.Data;

namespace QuizAppConsole.Services;
public class QuestionBuilderService
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
                throw new ArgumentNullException(string.Format(AppConstants.GENERAL_FILE_NOT_SPECIFIED, AppConstants.JSON_FORMAT_NAME));


            List<BookletQuestion> questions = JsonConvert.DeserializeObject<List<BookletQuestion>>(json);

            foreach (var question in questions)
            {
                if (!question.ValidateCorrectOptionCount())
                ConsoleHelper.WriteColoredLine(string.Format(AppConstants.INVALID_CORRECT_OPTION_COUNT_TEMPLATE, question.Id), ConsoleColors.Error);

                if (!question.ValidateIncorrectOptionCount())
                    ConsoleHelper.WriteColoredLine(string.Format(AppConstants.INVALID_INCORRECT_OPTION_COUNT_TEMPLATE, question.Id), ConsoleColors.Error);
            }



            return questions;
        }
        catch (JsonReaderException ex)
        {

            ConsoleHelper.WriteColored(string.Format(AppConstants.FILE_FORMAT_ERROR_MESSAGE_TEMPLATE, ex.Message, AppConstants.JSON_FORMAT_NAME), ConsoleColors.Error);
            return new List<BookletQuestion>();
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteColored(string.Format(AppConstants.FILE_FORMAT_ERROR_MESSAGE_TEMPLATE, ex.Message, AppConstants.JSON_FORMAT_NAME), ConsoleColors.Error);

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

                if (contentType != AppConstants.APPLICATION_JSON)
                    throw new InvalidOperationException(string.Format(AppConstants.UNEXPECTED_FILE_CONTENT_MESSAGE_TEMPLATE, AppConstants.JSON_FORMAT_NAME));


                if (response.IsSuccessStatusCode)
                    return LoadQuestionsFromJson(jsonFilePath: null, jsonSource: await response.Content.ReadAsStringAsync());
                else
                    throw new Exception(string.Format(AppConstants.DATA_FETCH_ERROR_MESSAGE_TEMPLATE, response.StatusCode));

            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteColoredLine(string.Format(AppConstants.ERROR_OCCURRED_MESSAGE, ex.Message), ConsoleColors.Error);
            return new List<BookletQuestion>();
        }
    }

}