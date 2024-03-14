using quiz_console_app.ViewModels;

namespace quiz_console_app.Services;

public class QuizDisplay
{
    public static void DisplayBooklets(List<BookletViewModel> booklets)
    {
        foreach (var booklet in booklets)
        {
            Console.WriteLine();
            Console.WriteLine("Kitapçık Id: " + booklet.Id);
            Console.WriteLine("Kitapçık Adı: " + booklet.BookletName);

            foreach (var question in booklet.Questions)
            {
                DisplayQuestion(question);
            }

            Console.WriteLine();
        }
    }

    private static void DisplayQuestion(QuestionViewModel question)
    {
        Console.WriteLine();
        Console.WriteLine("Soru :" + question.AskText);

        foreach (var questionOption in question.QuestionOptions)
        {
            Console.Write(questionOption.Text + " ");
        }
        Console.WriteLine();
    }
}

