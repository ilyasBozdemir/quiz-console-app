using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.Services;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Screens;

public class QuizModeScreen
{
    private readonly QuizService _quizService;
    private readonly AnswerKeyCollection _answerKeys;
    private readonly List<User> _users;
    private List<UserAnswerKeyViewModel> _userAnswers;

    private User _user;
    private UserQuiz _userQuiz;
    private Quiz _quiz;
    private DateTime _quizEndTime;
    private DateTime _quizStartTime;
    private TimeSpan _quizDuration;
    private Timer _timer;

    public QuizModeScreen()
    {
        _quizService = new QuizService();
        _quizService.GenerateBooklets();
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

        _users = new List<User>();
    }

    private User GetUserFromConsoleInput()
    {
        ConsoleHelper.WriteColored(
            "Lütfen isminizi ve soyisminizi aralarında boşluk bırakarak girin: ",
            ConsoleColors.Info
        );
        string fullName = Console.ReadLine();
        string[] nameParts = fullName.Split(
            new char[] { ' ' },
            StringSplitOptions.RemoveEmptyEntries
        );
        string firstName = nameParts.Length > 0 ? nameParts[0] : null;
        string lastName = nameParts.Length > 1 ? nameParts[1] : null;

        ConsoleHelper.WriteColored("Lütfen kullanıcı adınızı girin: ", ConsoleColors.Info);
        string username = Console.ReadLine();

        Console.ResetColor();

        User user = _users.FirstOrDefault(u => u.Username == username);

        if (user == null)
        {
            user = new User(firstName, lastName, username);
            user.Id = Guid.NewGuid();
            _users.Add(user);
        }

        return user;
    }

    private void CheckAndSetUser()
    {
        if (_user == null)
            _user = GetUserFromConsoleInput();
        else
        {
            QuizDisplay.DisplayUserInfo(_user);
            QuizDisplay.DisplaySeparator();
            ConsoleHelper.WriteColored(
                "Varolan bir kullanıcı bulundu. Yeniden giriş yapmak ister misiniz? (E/H): ",
                ConsoleColors.Info
            );
            string input = Console.ReadLine().ToUpper();
            if (input == "E")
                _user = GetUserFromConsoleInput();
        }
    }

    public void StartQuiz()
    { 
        var Booklet = QuizService.Booklets.FirstOrDefault();
        int questionNumber = 1;
        int userBookletId = 1;
        Booklet.Id = userBookletId;

        _quiz.Title = "Yazılım Bilgisi Quiz'i";
        _quiz.Description = "Bu quiz, yazılım geliştirmeyle ilgili genel bilginizi test etmek için hazırlanmıştır. ";
        _quiz.Creator = "@QuizConsoleApp";

        _quiz.ScoringRules = new ScoringRules(Booklet.Questions[0].QuestionOptions.Count);

        CheckAndSetUser();

        _userQuiz = new UserQuiz
        {
            Id = Guid.NewGuid(),
            StartTime = _quizStartTime,
            QuizId = _quiz.Id,
            User = _user,
            UserId = _user.Id
        };

        _userQuiz.StartTime = _userQuiz.StartTime.Add(_quizDuration);

        foreach (var question in Booklet.Questions)
        {
            ConsoleHelper.WriteColoredLine($"{questionNumber}. Soru: ", ConsoleColors.Info);
            ConsoleHelper.WriteColoredLine(question.AskText, ConsoleColors.Default);

            for (int i = 0; i < question.QuestionOptions.Count; i++)
            {
                char optionLetter = OptionHelper.ToOptionLetter(i);

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
                && userAnswerFromReadLine[0] >= OptionHelper.ToOptionLetter(0)
                && userAnswerFromReadLine[0] <= OptionHelper.ToOptionLetter(question.QuestionOptions.Count - 1)
            )
            {

                var optionChar = userAnswerFromReadLine[0];
                var id = OptionHelper.FromOptionLetter(optionChar);

                _userAnswers.Add(
                    new UserAnswerKeyViewModel
                    {
                        BookletId = Booklet.Id,
                        QuestionId = question.Id,
                        Id = question.Id,
                        UserAnswerOption = userAnswerFromReadLine,
                        UserAnswerOptionText= question.QuestionOptions[id].Text,
                        UserAnswerOptionId = OptionHelper.FromOptionLetter(optionChar)
                    }
                );
                questionNumber++;
                QuizDisplay.ClearConsole();
            }
            else if (userAnswerFromReadLine.Length == 0)
            {
                _userAnswers.Add(
                    new UserAnswerKeyViewModel
                    {
                        BookletId = Booklet.Id,
                        QuestionId = question.Id,
                        UserAnswerOption = null,
                        Id = question.Id,
                    }
                );
                questionNumber++;
                QuizDisplay.ClearConsole();
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

        QuizDisplay.ClearConsole();

        QuizDisplay.DisplayQuizAndUserData(_quiz, _user);

        QuizDisplay.DisplaySeparator();
        _quizService.EvaluateQuizResults(_userAnswers, userBookletId, _quiz.ScoringRules);

        if (AskForQuizRetry())
        {
            QuizDisplay.ClearConsole();
            RetryQuiz();
        }
    }

    public bool AskForQuizRetry()
    {
        Console.WriteLine();
        Console.Write("Testi tekrar çözmek ister misiniz? (E/H): ");
        string input = Console.ReadLine().Trim().ToUpper();

        while (input != "E" && input != "H")
        {
            Console.Write("Geçersiz giriş. Lütfen sadece 'E' veya 'H' girin: ");
            input = Console.ReadLine().Trim().ToUpper();
        }

        return input == "E";
    }

    public void EndQuiz(object state)
    {
        if (DateTime.Now >= _quizEndTime)
            _timer?.Dispose();
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

        _userAnswers = new List<UserAnswerKeyViewModel>();

        StartQuiz();
    }
}
