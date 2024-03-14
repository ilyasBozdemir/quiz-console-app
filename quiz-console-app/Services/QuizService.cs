using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Services;

public static class QuizService
{
    public static List<BookletViewModel> GenerateBooklets(List<Question> questions, int shuffleCount = 1)
    {
        List<Question> shuffledQuestions = new List<Question>();

        
        List<BookletViewModel> booklets = new List<BookletViewModel>();

        for (int i = 0; i < shuffleCount; i++)
        {
            List<Question> bookletQuestions = QuestionShuffler.ShuffleQuestionOptions(questions);

           

            BookletViewModel booklet = new BookletViewModel
            {
                Id = i + 1,
                BookletName = $"Booklet {i + 1}",
                Questions = bookletQuestions.Select(q => MapToQuestionViewModel(q)).ToList()
            };

            QuestionShuffler.ShuffleBookletQuestions(booklet);

            booklets.Add(booklet);
        }

        return booklets;
    }

    private static QuestionViewModel MapToQuestionViewModel(Question question)
    {
        return new QuestionViewModel
        {
            Id = question.Id,
            AskText = question.AskText,
            Explanation = question.Explanation,
            Difficulty = question.Difficulty,
            QuestionOptions = question.QuestionOptions.Select(o => MapToQuestionOptionViewModel(o)).ToList()
        };
    }

    private static QuestionOptionViewModel MapToQuestionOptionViewModel(QuestionOption option)
    {
        return new QuestionOptionViewModel
        {
            Id = option.Id,
            Text = option.Text
        };
    }
}
