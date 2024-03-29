﻿using QuizAppConsole.Constants;
using QuizAppConsole.Helpers;
using QuizAppConsole.Models;
using QuizAppConsole.Models.Quiz;
using QuizAppConsole.Services;
using QuizAppConsole.ViewModels;

namespace QuizAppConsole.Views;

public class QuizModeView
{
    private readonly QuizService _quizService = new QuizService();
    private readonly AnswerKeyCollection _answerKeys = new AnswerKeyCollection();
    private readonly List<User> _users = new List<User>();
    private List<UserAnswerKeyViewModel> _userAnswers= new List<UserAnswerKeyViewModel>();

    private User _user;
    private UserQuiz _userQuiz;
    private Quiz _quiz;
    private DateTime _quizEndTime;
    private DateTime _quizStartTime;
    private TimeSpan _quizDuration;
    private Timer _timer;

    public QuizModeView()
    {
        _quizService = new QuizService();
        _quizService.GenerateBooklets(1);
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
    }

    private User GetUserFromConsoleInput()
    {
        ConsoleHelper.WriteColored(
            "Lütfen isminizi ve soyisminizi aralarında boşluk bırakarak girin: ",
            ConsoleColors.Info
        );
        string fullName = Console.ReadLine() ?? "";
        string[] nameParts = fullName.Split(
            new char[] { ' ' },
            StringSplitOptions.RemoveEmptyEntries
        );
        string firstName = nameParts.Length > 0 ? nameParts[0] : "";
        string lastName = nameParts.Length > 1 ? nameParts[1] : "";

        ConsoleHelper.WriteColored("Lütfen kullanıcı adınızı girin: ", ConsoleColors.Info);
        string username = Console.ReadLine() ?? "";

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
            QuizConsoleDisplayService.DisplayUserInfo(_user);
            QuizConsoleDisplayService.DisplaySeparator();
            ConsoleHelper.WriteColored(
                "Varolan bir kullanıcı bulundu. Yeniden giriş yapmak ister misiniz? (E/H): ",
                ConsoleColors.Info
            );
            string input = Console.ReadLine() ?? "";

            input = input.ToUpper();

            if (input == "E")
                _user = GetUserFromConsoleInput();
        }
    }

    public void StartQuiz()
    {
        var Booklet = QuizService.Booklets.FirstOrDefault() ?? new BookletViewModel();
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
            string userAnswerFromReadLine = Console.ReadLine() ?? "";
            userAnswerFromReadLine= userAnswerFromReadLine.ToUpper();
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
                        UserAnswerOptionText = question.QuestionOptions[id].Text,
                        UserAnswerOptionId = OptionHelper.FromOptionLetter(optionChar)
                    }
                );
                questionNumber++;
                QuizConsoleDisplayService.ClearConsole();
            }
            else if (userAnswerFromReadLine.Length == 0)
            {
                _userAnswers.Add(
                    new UserAnswerKeyViewModel
                    {
                        BookletId = Booklet.Id,
                        QuestionId = question.Id,
                        UserAnswerOption = "",
                        Id = question.Id,
                    }
                );
                questionNumber++;
                QuizConsoleDisplayService.ClearConsole();
            }
            else
            {
                char optionLetter = (char)64;
                string optionsText = "";
                for (int i = 0; i < question.QuestionOptions.Count; i++)
                {
                    optionLetter++;
                    optionsText +=
                        i != 0
                            ? i != question.QuestionOptions.Count - 1
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

        QuizConsoleDisplayService.ClearConsole();

        QuizConsoleDisplayService.DisplayQuizAndUserData(_quiz, _user);

        QuizConsoleDisplayService.DisplaySeparator();
        _quizService.EvaluateQuizResults(_userAnswers, userBookletId, _quiz.ScoringRules);

        if (AskForQuizRetry())
        {
            QuizConsoleDisplayService.ClearConsole();
            RetryQuiz();
        }
    }

    public bool AskForQuizRetry()
    {
        Console.WriteLine();
        ConsoleHelper.WriteColored($"Testi tekrar çözmek için ", ConsoleColors.Info);
        ConsoleHelper.WriteColored($"(R)", ConsoleColors.Success);
        ConsoleHelper.WriteColored($", çıkmak için ", ConsoleColors.Info);
        ConsoleHelper.WriteColored($"(E)", ConsoleColors.Error);
        ConsoleHelper.WriteColored($", ana menüye dönmek için ", ConsoleColors.Info);
        ConsoleHelper.WriteColored($"(H) ", ConsoleColors.Default);
        ConsoleHelper.WriteColored($"tuşlayın.", ConsoleColors.Info);
        Console.WriteLine();

        string input = Console.ReadLine() ?? "";

        input = input.Trim().ToUpper();

        while (input != "E" && input != "H" && input != "R")
        {
            Console.Write("Geçersiz giriş. Lütfen sadece 'E', 'H' veya 'R' girin: ");
            input = Console.ReadLine() ?? "";
            input = input.Trim().ToUpper();
        }

        if (input == "E")
            Environment.Exit(0);
        else if (input == "H")
            new QuizMainMenuView().Show();

        else if (input == "R")
            return true;

        return false;
    }


    public void EndQuiz(object? state)
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

        _timer = new Timer(EndQuiz, "", _quizDuration, TimeSpan.Zero);

        _userAnswers = new List<UserAnswerKeyViewModel>();

        StartQuiz();
    }
}
