using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.Services;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Screens;

public class QuizModeScreen
{
    private readonly QuizService _quizService;
    private readonly QuestionLoader _questionLoader;
    private readonly List<Question> _questions;
    private readonly List<UserAnswerKeyViewModel> _userAnswers;
    private readonly List<AnswerKeyViewModel> _answerKeys;

    public QuizModeScreen()
    {
        _questionLoader = new QuestionLoader();
        _questions = _questionLoader.LoadQuestionsFromJson("software_questions.json");
        _quizService = new QuizService();
        _quizService.GenerateBooklets(_questions, 1);
        _userAnswers = new List<UserAnswerKeyViewModel>();
        _answerKeys = QuizService.AnswerKeys;
    }

    public void Start()
    {
        var Booklet = QuizService.Booklets.FirstOrDefault();
        int questionNumber = 1;
        foreach (var question in Booklet.Questions)
        {
            ConsoleHelper.WriteColoredLine($"{questionNumber}. Soru: ", ConsoleColors.Info);
            ConsoleHelper.WriteColoredLine(question.AskText, ConsoleColors.Default);

            for (int i = 0; i < question.QuestionOptions.Count; i++)
            {
                char optionLetter = (char)(65 + i);

                ConsoleHelper.WriteColored($"{optionLetter})", ConsoleColors.Info);
                ConsoleHelper.WriteColored(
                    $"{question.QuestionOptions[i].Text} ",
                    ConsoleColors.Default
                );
            }
            Console.WriteLine();
            Console.Write("Cevabınızı girin (Boş bırakmak için Enter tuşuna basın) : ");
            string userAnswerFromReadLine = Console.ReadLine().ToUpper();
            Console.WriteLine();
            if (
                userAnswerFromReadLine.Length == 1
                && userAnswerFromReadLine[0] >= (char)65
                && userAnswerFromReadLine[0] <= (char)(65 + question.QuestionOptions.Count - 1)
            )
            {
                _userAnswers.Add(
                    new UserAnswerKeyViewModel
                    {
                        BookletId = Booklet.Id,
                        QuestionId = question.Id,
                        UserAnswerOption = userAnswerFromReadLine
                    }
                );
                questionNumber++;
            }
            else if (userAnswerFromReadLine.Length == 0) // cevap null ise
            {
                _userAnswers.Add(
                    new UserAnswerKeyViewModel
                    {
                        BookletId = Booklet.Id,
                        QuestionId = question.Id,
                        UserAnswerOption = null
                    }
                );
                questionNumber++;
            }
            else
            {
                char optionLetter = (char)64;
                string optionsText = "";
                for (int i = 0; i < question.QuestionOptions.Count; i++)
                {
                    optionLetter++;
                    optionsText +=
                        (i != 0)
                            ? (i != question.QuestionOptions.Count - 1)
                                ? $", {optionLetter}"
                                : $" veya {optionLetter}"
                            : $"{optionLetter}";
                }

                string errorMessage =
                    $"Geçersiz cevap. Lütfen {optionsText} harflerinden birini girin. veya boş bırakmak için sadece Enter tuşuna basın.";

                ConsoleHelper.WriteColoredLine(errorMessage, ConsoleColors.Error);
            }
        }
        _quizService.EvaluateQuizResults(_userAnswers);
    }
}
