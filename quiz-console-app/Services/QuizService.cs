using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Services;

public class QuizService
{
    public static List<BookletViewModel> Booklets { get; private set; }
    public static List<AnswerKeyViewModel> AnswerKeys { get; private set; }
    public static List<BookletQuestion> Questions { get; private set; }
    private int maxShuffleCount = 10;
    public QuizService()
    {
        Booklets = new List<BookletViewModel>();
        AnswerKeys = new List<AnswerKeyViewModel>();
    }
    public void GenerateBooklets(List<BookletQuestion> questions, int shuffleCount = 1)
    {

        shuffleCount = Math.Min(shuffleCount, maxShuffleCount);
        if (shuffleCount > maxShuffleCount)
            ConsoleHelper.WriteColoredLine($"İzin verilen toplam kitapçık sayısı {maxShuffleCount}", ConsoleColors.Warning);
        
        Questions = questions;

        List<BookletQuestion> shuffledQuestions = new List<BookletQuestion>();

        List<BookletViewModel> booklets = new List<BookletViewModel>();


        for (int i = 0; i < shuffleCount; i++)
        {
            char prefix = (char)(65 + i);
            string bookletName = $"{prefix}_{1}"; 

            List<BookletQuestion> bookletQuestions = QuestionShuffler.ShuffleQuestionOptions(questions);

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
        QuizResultSummary resultSummary = new QuizResultSummary(userAnswers.Count);

        ConsoleHelper.WriteColoredLine($"Cevaplar", ConsoleColors.Title);
        foreach (var userAnswer in userAnswers)
        {
            string formattedQuestionNumber = FormattingHelper.FormatQuestionNumber(resultSummary.QuestionCurrentNumber, resultSummary.TotalQuestions);
            ConsoleHelper.WriteColored($"{formattedQuestionNumber})", ConsoleColors.Default);
            
            if (string.IsNullOrEmpty(userAnswer.UserAnswerOption))
            {
                resultSummary.BlankCount++;
                ConsoleHelper.WriteColored(" - Boş", ConsoleColors.Warning);
            }
            else
            {
                var correspondingQuestion = Booklets
                    .SelectMany(booklet => booklet.Questions)
                    .FirstOrDefault(question => question.Id == userAnswer.QuestionId);

                if (correspondingQuestion != null)
                {
                    var correctOption = correspondingQuestion.QuestionOptions
                        .FirstOrDefault(option => option.IsCorrect && option.Text == userAnswer.UserAnswerOption);

                    if (correctOption != null)
                    {
                        ConsoleHelper.WriteColored($"{userAnswer.UserAnswerOption} ", ConsoleColors.Info);
                        ConsoleHelper.WriteColored(" - Doğru", ConsoleColors.Success);
                        resultSummary.CorrectCount++;
                    }
                    else
                    {
                        ConsoleHelper.WriteColored($"{userAnswer.UserAnswerOption} ", ConsoleColors.Info);
                        ConsoleHelper.WriteColored(" - Yanlış", ConsoleColors.Error);
                        resultSummary.IncorrectCount++;
                    }
                }
            }
            Console.WriteLine();
            resultSummary.QuestionCurrentNumber++;
        }
        QuizDisplay.EvaluateQuizResults(resultSummary);
    }
    private static QuestionViewModel MapToQuestionViewModel(BookletQuestion question)
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
    private static QuestionOptionViewModel MapToQuestionOptionViewModel(BookletQuestionOption option)
    {
        return new QuestionOptionViewModel
        {
            Id = option.Id,
            Text = option.Text
        };
    }
}
