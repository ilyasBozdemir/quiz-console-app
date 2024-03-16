using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.Services;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Screens;

public class DisplayBookletScreen
{
    private readonly QuizService _quizService;
    private readonly QuestionLoader _questionLoader;
    private readonly List<Question> _questions;
    private readonly List<UserAnswerKeyViewModel> _userAnswers;
    private readonly List<AnswerKeyViewModel> _answerKeys;
    public DisplayBookletScreen()
    {
        _questionLoader = new QuestionLoader();
        _questions = _questionLoader.LoadQuestionsFromJson("software_questions.json");
        _quizService = new QuizService();
        _quizService.GenerateBooklets(_questions, 1);
        _userAnswers = new List<UserAnswerKeyViewModel>();
        _answerKeys = QuizService.AnswerKeys;
    }

    public  void Start()
    {

    }
}
