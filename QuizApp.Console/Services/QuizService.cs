using QuizAppConsole.Constants;
using QuizAppConsole.Helpers;
using QuizAppConsole.Models;
using QuizAppConsole.Models.Data;
using QuizAppConsole.Models.Quiz;
using QuizAppConsole.ViewModels;

namespace QuizAppConsole.Services;

public class QuizService
{
    public static List<BookletViewModel> Booklets { get; private set; } = new List<BookletViewModel>();
    public static AnswerKeyCollection AnswerKeys { get; private set; } = new AnswerKeyCollection();

    private List<BookletQuestion> _sourceQuestions;
    private readonly QuestionBuilderService _questionLoader;

    public QuizService()
    {
        _questionLoader = new QuestionBuilderService();
        Booklets = new List<BookletViewModel>();
        AnswerKeys = new AnswerKeyCollection();

        if (_sourceQuestions == null)
        {
            ConsoleHelper.WriteColoredLine(AppConstants.QUESTIONS_ARE_BEING_FETCHED_MESSAGE, ConsoleColors.Info);
            InitializeAsync().Wait();
            Console.Clear();
            ConsoleHelper.WriteColoredLine(AppConstants.QUESTIONS_FETCHED_SUCCESSFULLY_MESSAGE, ConsoleColors.Info);
            Thread.Sleep(500);
            Console.Clear();
        }
        else
        {
            ConsoleHelper.WriteColoredLine(AppConstants.QUESTIONS_LOAD_FAILED_MESSAGE, ConsoleColors.Error);
        }



    }
    private async Task InitializeAsync()
    {
        Uri apiUrl = new Uri(AppConstants.JSON_API_URL);

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

            List<BookletQuestion> bookletQuestions = QuestionOptionsSuffleService.ShuffleQuestionOptions(_sourceQuestions);

            BookletViewModel booklet = new BookletViewModel
            {
                Id = i + 1,
                BookletName = $"{AppConstants.DEFAULT_BOOKLET_NAME} {i + 1}",
                Questions = bookletQuestions.Select(q => MapToQuestionViewModel(q)).ToList(),
                Prefix = bookletName,

            };
            QuestionOptionsSuffleService.ShuffleBookletQuestions(booklet);
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
                        Id = 0,//db olmadıgı için 0 hepsine
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
            ConsoleHelper.WriteColoredLine(AppConstants.ANSWERS_LABEL, ConsoleColors.Title);

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
                            ConsoleHelper.WriteColored(AppConstants.BLANK_ANSWER, ConsoleColors.Warning);
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
                                    ConsoleHelper.WriteColored($"{OptionHelper.ToOptionLetter(userAnswers[j].UserAnswerOptionId)} {AppConstants.CORRECT_ANSWER}", ConsoleColors.Success);
                                    resultSummary.CorrectCount++;
                                    Console.WriteLine();
                                }
                                else
                                {
                                    ConsoleHelper.WriteColored($"{OptionHelper.ToOptionLetter(userAnswers[j].UserAnswerOptionId)} {AppConstants.INCORRECT_ANSWER}", ConsoleColors.Error);
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
            ConsoleHelper.WriteColoredLine(string.Format(AppConstants.BOOKLET_ID_NOT_FOUND_ERROR_MESSAGE_TEMPLATE, userBookletId), ConsoleColors.Error);

        QuizConsoleDisplayService.EvaluateQuizResults(resultSummary);
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
