using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Services;

public class QuizService
{
    public static List<BookletViewModel> Booklets { get; private set; }
    public static List<AnswerKeyViewModel> AnswerKeys { get; private set; }
    public static List<Question> Questions { get; private set; }

    private int maxShuffleCount = 10;
    public QuizService()
    {
        Booklets = new List<BookletViewModel>();
        AnswerKeys = new List<AnswerKeyViewModel>();
    }

    public void GenerateBooklets(List<Question> questions, int shuffleCount = 1)
    {

        shuffleCount = Math.Min(shuffleCount, maxShuffleCount);
        if (shuffleCount > maxShuffleCount)
            ConsoleHelper.WriteColoredLine($"İzin verilen toplam kitapçık sayısı {maxShuffleCount}", ConsoleColors.Warning);
        
        Questions = questions;

        List<Question> shuffledQuestions = new List<Question>();

        List<BookletViewModel> booklets = new List<BookletViewModel>();


        for (int i = 0; i < shuffleCount; i++)
        {
            char prefix = (char)(65 + i);
            string bookletName = $"{prefix}_{1}"; 

            List<Question> bookletQuestions = QuestionShuffler.ShuffleQuestionOptions(questions);

            BookletViewModel booklet = new BookletViewModel
            {
                Id = i + 1,
                BookletName = $"Booklet {i + 1}",
                Questions = bookletQuestions.Select(q => MapToQuestionViewModel(q)).ToList(),
                Prefix = bookletName
            };
            QuestionShuffler.ShuffleBookletQuestions(booklet);
            booklets.Add(booklet);
        }

        Booklets = booklets;
    }

    public  List<AnswerKeyViewModel> GenerateAnswerKeys()
    {
        List<AnswerKeyViewModel> answerKeys = new List<AnswerKeyViewModel>();

        foreach (var question in Questions)
        {
            var correctOption = question.QuestionOptions.FirstOrDefault(option => option.IsCorrect);

            if (correctOption != null)
            {
                answerKeys.Add(new AnswerKeyViewModel
                {
                    BookletId = 1,
                    QuestionId = question.Id,
                    CorrectOptionText = correctOption.Text
                });
            }
        }

        return answerKeys;
    }


    public void EvaluateQuizResults(List<UserAnswerKeyViewModel> userAnswers)
    {
        
    }


    private static QuestionViewModel MapToQuestionViewModel(Question question)
    {
        return new QuestionViewModel
        {
            Id = question.Id,
            AskText = question.AskText,
            Explanation = question.Explanation,
            Difficulty = question.Difficulty,
            QuestionOptions = question.QuestionOptions
            .Select(o => MapToQuestionOptionViewModel(o)).ToList()
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
