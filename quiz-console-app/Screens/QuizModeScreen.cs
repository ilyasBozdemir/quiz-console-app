using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.Services;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Screens;

public class QuizModeScreen
{
    private readonly QuizService _quizService;
    private readonly QuestionLoader _questionLoader;
    private readonly List<BookletQuestion> _questions;
    private readonly List<UserAnswerKeyViewModel> _userAnswers;
    private readonly List<AnswerKeyViewModel> _answerKeys;
    private readonly List<User> _users;

    private User _user;
    private UserQuiz _userQuiz;

    private Quiz _quiz;
    private DateTime _quizEndTime;
    private DateTime _quizStartTime;
    private TimeSpan _quizDuration;
    private Timer _timer;
    public QuizModeScreen()
    {
        _questionLoader = new QuestionLoader();
        _questions = _questionLoader.LoadQuestionsFromJson(jsonFilePath: "software_questions.json");
        _quizService = new QuizService();
        _quizService.GenerateBooklets(_questions, 1);
        _userAnswers = new List<UserAnswerKeyViewModel>();
        _answerKeys = QuizService.AnswerKeys;
        _quiz = new Quiz(options =>
        {
            options.DurationInMinutes = 20; 
            options.IsOpenToPublic = true;  
        });
        _quizStartTime = DateTime.Now;
        _quizDuration = TimeSpan.FromMinutes(_quiz.DurationInMinutes);
        _quizEndTime = _quizStartTime.Add(_quizDuration);
        _timer = new Timer(EndQuiz, null, _quizDuration, TimeSpan.Zero);

        _users= new List<User>();
    }

    public void StartQuiz()
    {

        _quiz.Title = "Yazılım Bilgisi Quiz'i";
        _quiz.Description = "Bu quiz, yazılım geliştirmeyle ilgili genel bilginizi test etmek için hazırlanmıştır. Programlama dillerinden, algoritmik düşünceye kadar birçok konuyu içerebilir.";
        _quiz.Creator = "@QuizConsoleApp";


        ConsoleHelper.WriteColored(" Soru Id: ", ConsoleColors.Info);
        Console.Write("Lütfen isminizi ve soyisminizi aralarında boşluk bırakarak girin:");
        string fullName = Console.ReadLine();
        string[] nameParts = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string firstName = nameParts.Length > 0 ? nameParts[0] : null;
        string lastName = nameParts.Length > 1 ? nameParts[1] : null;

        // Kullanıcı adını al
        Console.Write("Lütfen kullanıcı adınızı girin:");
        string username = Console.ReadLine();

        Console.ResetColor();

        _user = _users.FirstOrDefault(u => u.Username == username);

        if (_user == null)
        {
            _user = new User(firstName, lastName, username);
            _users.Add(_user);
        }

        _userQuiz = new UserQuiz
        {
            Id = Guid.NewGuid(),
            StartTime = _quizStartTime,
            QuizId = _quiz.Id,
            User = _user,
            UserId = _user.Id
        };

        _userQuiz.StartTime = _userQuiz.StartTime.Add(_quizDuration);

        //
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
            Console.Write($"Cevabınızı girin (Boş bırakmak için Enter tuşuna basın) : ");

            Console.ForegroundColor = ConsoleColors.Debug;
            string userAnswerFromReadLine = Console.ReadLine().ToUpper();
            ConsoleHelper.WriteColoredLine(question.AskText, ConsoleColors.Default);
            Console.ResetColor();
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
                Console.Clear();
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
                Console.Clear();
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

        _userQuiz.Quiz = _quiz;
        _userQuiz.IsCompleted = true;

        _user.UserQuizzes.Add(_userQuiz);
        _quizService.EvaluateQuizResults(_userAnswers);

    }


    public void EndQuiz(object state)
    {
        if (DateTime.Now >= _quizEndTime)
            _timer.Dispose();
    }


    public void RetryQuiz()
    {
        _quiz.ConfigureQuiz(options =>
        {
            options.DurationInMinutes = 20;
            options.IsOpenToPublic = true;
        });

        _quizStartTime = DateTime.Now;
        _quizDuration = TimeSpan.FromMinutes(_quiz.DurationInMinutes);

        _timer?.Dispose();

        _timer = new Timer(EndQuiz, null, _quizDuration, TimeSpan.Zero);

      
        StartQuiz();
    }

}