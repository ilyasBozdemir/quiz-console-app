using quiz_console_app.Models;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Helpers;

public class QuestionShuffler
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


    public static List<Question> ShuffleQuestionOptions(List<Question> questions)
    {
  
        foreach (var question in questions)
        {
            List<QuestionOption> shuffledOptions = ShuffleOptions(question.QuestionOptions, random);
            question.QuestionOptions = shuffledOptions;
        }

        return questions;
    }


    private static List<QuestionOption> ShuffleOptions(List<QuestionOption> options, Random random)
    {
        for (int i = options.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            QuestionOption temp = options[i];
            options[i] = options[j];
            options[j] = temp;
        }

        return options;
    }
}
