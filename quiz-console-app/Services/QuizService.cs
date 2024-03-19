using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Services;

public class QuizService
{
    public static List<BookletViewModel> Booklets { get; private set; }
    public static AnswerKeyCollection AnswerKeys { get; private set; }

    private List<BookletQuestion> _sourceQuestions;
    private readonly QuestionLoader _questionLoader;
 
    public QuizService()
    {
        _questionLoader = new QuestionLoader();
        Booklets = new List<BookletViewModel>();
        AnswerKeys = new AnswerKeyCollection();

        InitializeAsync().GetAwaiter().GetResult();

    }
    private async Task InitializeAsync()
    {
        Uri apiUrl = new Uri("https://quiz-app-data-api.vercel.app/api/get-questions");
        _sourceQuestions = await _questionLoader.LoadQuestionsFromUrl(apiUrl);
    }

    public void GenerateBooklets(int shuffleCount = 1)
    {
        List<BookletQuestion> shuffledQuestions = new List<BookletQuestion>();
        List<BookletViewModel> booklets = new List<BookletViewModel>();

        for (int i = 0; i < shuffleCount; i++)
        {
            char prefix = (char)(65 + i);
            string bookletName = $"{prefix}_{1}";

            List<BookletQuestion> bookletQuestions = QuestionShuffler.ShuffleQuestionOptions(_sourceQuestions);

            BookletViewModel booklet = new BookletViewModel
            {
                Id = i + 1,
                BookletName = $"Booklet {i + 1}",
                Questions = bookletQuestions.Select(q => MapToQuestionViewModel(q)).ToList(),
                Prefix = bookletName,

            };
            QuestionShuffler.ShuffleBookletQuestions(booklet);
            GenerateAnswerKeys(booklet);
            booklets.Add(booklet);
        }

        Booklets = booklets;

    }

    public void GenerateAnswerKeys(BookletViewModel booklet)
    {
        List<AnswerKeyViewModel> questionAnswerKeys = new List<AnswerKeyViewModel>();

        foreach (var question in booklet.Questions)
        {
            foreach (var option in question.QuestionOptions)
            {
                if (option.IsCorrect)
                {
                    AnswerKeyViewModel answerKey = new AnswerKeyViewModel
                    {
                        BookletId = booklet.Id,
                        QuestionId = question.Id,
                        CorrectOptionText = option.Text
                    };
                    questionAnswerKeys.Add(answerKey);
                }
            }
        }

        AnswerKeys.AddAnswerKey(booklet.Id, questionAnswerKeys);
    }




    public void EvaluateQuizResults(List<UserAnswerKeyViewModel> userAnswers, int userBookletId, ScoringRules scoringRules)
    {
        bool allAnswersInSameBooklet = true;
        for (int i = 0; i < userAnswers.Count; i++)

            if (userAnswers[i].BookletId != userBookletId)
            {
                allAnswersInSameBooklet = false;
                break;
            }

        QuizResultSummary resultSummary = new QuizResultSummary(userAnswers.Count, scoringRules);

        List<AnswerKeyViewModel> answerKeys = AnswerKeys.GetAnswerKeys(userBookletId);
   

        if (allAnswersInSameBooklet)
        {
            ConsoleHelper.WriteColoredLine($"Cevaplar", ConsoleColors.Title);

            for (int i = 0; i < answerKeys.Count; i++)
            {
                for (int j = 0; j < userAnswers.Count; j++)
                {
                    if (i == j)
                    {
                        string formattedQuestionNumber =
                            FormattingHelper
                            .FormatQuestionNumber(resultSummary.QuestionCurrentNumber, resultSummary.TotalQuestions);

                        ConsoleHelper.WriteColored($"{formattedQuestionNumber})", ConsoleColors.Default);

                        if (string.IsNullOrEmpty(userAnswers[i].UserAnswerOption))
                        {
                            resultSummary.BlankCount++;
                            ConsoleHelper.WriteColored(" - Boş", ConsoleColors.Warning);
                            Console.WriteLine();
                        }
                        else
                        {
                            var correspondingQuestion = Booklets
                                .SelectMany(booklet => booklet.Questions)
                                .FirstOrDefault(question => question.Id == userAnswers[j].QuestionId);

                            if (correspondingQuestion != null)
                            {
                                var correctOption = correspondingQuestion
                                       .QuestionOptions
                                       .FirstOrDefault(option => option.IsCorrect);

                                if (answerKeys[i].CorrectOptionText == userAnswers[j].UserAnswerOptionText)
                                {
                                    ConsoleHelper.WriteColored($"{OptionHelper.ToOptionLetter(userAnswers[j].UserAnswerOptionId)} - Doğru", ConsoleColors.Success);
                                    resultSummary.CorrectCount++;
                                    Console.WriteLine();
                                }
                                else
                                {
                                    ConsoleHelper.WriteColored($"{OptionHelper.ToOptionLetter(userAnswers[j].UserAnswerOptionId)} - Yanlış", ConsoleColors.Error);
                                    resultSummary.IncorrectCount++;
                                    Console.WriteLine();
                                }

                            }
                        }
                        resultSummary.QuestionCurrentNumber++;
                    }
                }
            }
        }
        else
            ConsoleHelper.WriteColoredLine($"Hata: Beklenen kitapçık ID bulunamadı. Beklenen ID: {userBookletId}", ConsoleColors.Error);

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
            .Select(o => MapToQuestionOptionViewModel(o)).ToList(),

        };
    }
    private static QuestionOptionViewModel MapToQuestionOptionViewModel(BookletQuestionOption option)
    {
        return new QuestionOptionViewModel
        {
            Id = option.Id,
            Text = option.Text,
            IsCorrect = option.IsCorrect,
        };
    }
}
