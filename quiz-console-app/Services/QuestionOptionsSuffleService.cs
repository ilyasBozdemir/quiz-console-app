using quiz_console_app.Models;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Services;

public class QuestionOptionsSuffleService
{
    private static Random random = new Random();

    public static List<Booklet> ShuffleBooklets(List<Booklet> booklets)
    {
        return booklets.OrderBy(b => random.Next()).ToList();
    }

    public static void ShuffleBookletQuestions(BookletViewModel booklet)
    {
        booklet.Questions = booklet.Questions.OrderBy(q => random.Next()).ToList();
    }

    public static List<BookletQuestion> ShuffleQuestionOptions(List<BookletQuestion> questions)
    {

        foreach (var question in questions)
        {
            List<BookletQuestionOption> shuffledOptions = ShuffleOptions(question.QuestionOptions, random);
            question.QuestionOptions = shuffledOptions;
        }

        return questions;
    }


    private static List<BookletQuestionOption> ShuffleOptions(List<BookletQuestionOption> options, Random random)
    {
        for (int i = options.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            BookletQuestionOption temp = options[i];
            options[i] = options[j];
            options[j] = temp;
        }

        return options;
    }
}
