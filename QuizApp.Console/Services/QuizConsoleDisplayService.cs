using QuizAppConsole.Constants;
using QuizAppConsole.Helpers;
using QuizAppConsole.Models;
using QuizAppConsole.Models.Quiz;
using QuizAppConsole.ViewModels;

namespace QuizAppConsole.Services;

public class QuizConsoleDisplayService
{
    public static void DisplayBooklets(List<BookletViewModel> booklets)
    {
        foreach (var booklet in booklets)
        {
            ConsoleHelper.WriteColored(AppConstants.BOOKLET_ID_LABEL, ConsoleColors.Info);
            ConsoleHelper.WriteColoredLine(booklet.Id, ConsoleColors.Default);

            ConsoleHelper.WriteColored(AppConstants.BOOKLET_NAME_LABEL, ConsoleColors.Info);
            ConsoleHelper.WriteColoredLine(booklet.BookletName, ConsoleColors.Default);

            foreach (var question in booklet.Questions)
                DisplayQuestion(question);

            Console.WriteLine();
        }
    }

    private static void DisplayQuestion(QuestionViewModel question)
    {
        Console.WriteLine();
        ConsoleHelper.WriteColored(AppConstants.QUESTION_LABEL, ConsoleColors.Prompt);
        ConsoleHelper.WriteColoredLine(question.AskText, ConsoleColors.Default);

        foreach (var questionOption in question.QuestionOptions)
        {
            Console.Write(questionOption.Text + " ");
        }
        Console.WriteLine();
    }

    public static void DisplayAnswerKeys(List<AnswerKeyViewModel> answerKeys)
    {
        if (answerKeys == null || answerKeys.Count == 0)
        {
            ConsoleHelper.WriteColoredLine(AppConstants.ANSWER_KEYS_NOT_FOUND_MESSAGE, ConsoleColors.Error);
            return;
        }

        ConsoleHelper.WriteColored(AppConstants.ANSWER_KEYS_TITLE, ConsoleColors.Info);

        int questionNumber = 1;

        foreach (var answerKey in answerKeys)
        {
            ConsoleHelper.WriteColored(AppConstants.BOOKLET_ID_LABEL, ConsoleColors.Info);
            ConsoleHelper.WriteColoredLine(answerKey.BookletId, ConsoleColors.Default);

            ConsoleHelper.WriteColored(AppConstants.QUESTION_ID_LABEL, ConsoleColors.Info);
            ConsoleHelper.WriteColoredLine(answerKey.QuestionId, ConsoleColors.Default);

            ConsoleHelper.WriteColored(AppConstants.CORRECT_OPTION_LABEL, ConsoleColors.Info);
            ConsoleHelper.WriteColored(answerKey.CorrectOptionText, ConsoleColors.Default);

            ConsoleHelper.WriteColoredLine(answerKey.CorrectOptionText, ConsoleColors.Default);

            questionNumber++;
        }
    }

    public static void EvaluateQuizResults(QuizResultSummary resultSummary)
    {
        Console.WriteLine();
        ConsoleHelper.WriteColoredLine($"{AppConstants.TOTAL_QUESTION_COUNT_MESSAGE}{resultSummary.TotalQuestions}", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"{AppConstants.CORRECT_COUNT_MESSAGE}{resultSummary.CorrectCount}", ConsoleColors.Success);
        ConsoleHelper.WriteColoredLine($"{AppConstants.INCORRECT_COUNT_MESSAGE}{resultSummary.IncorrectCount}", ConsoleColors.Error);
        ConsoleHelper.WriteColoredLine($"{AppConstants.INCORRECT_ANSWER_PENALTY_MESSAGE}{resultSummary.ScoringRules.PenaltyForIncorrectAnswer}", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"{AppConstants.BLANK_COUNT_MESSAGE}{resultSummary.BlankCount}", ConsoleColors.Warning);
        ConsoleHelper.WriteColoredLine($"{AppConstants.NET_COUNT_MESSAGE}{resultSummary.NetCount:F2}/{resultSummary.TotalQuestions}", ConsoleColors.Default);
        ConsoleHelper.WriteColoredLine($"{AppConstants.SUCCESS_RATE_MESSAGE}{resultSummary.NetCount / resultSummary.TotalQuestions * 100:F2}%", ConsoleColors.Default);
        Console.WriteLine();
    }


    public static void DisplayQuizAndUserData(Quiz quiz, User user)
    {

        ConsoleHelper.WriteColoredLine(AppConstants.QUIZ_INFO_TITLE, ConsoleColors.Title);

        ConsoleHelper.WriteColored(AppConstants.QUIZ_CREATOR_LABEL, ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine(quiz.Creator, ConsoleColors.Default);

        ConsoleHelper.WriteColored(AppConstants.QUIZ_TITLE_LABEL, ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine(quiz.Title, ConsoleColors.Default);

        ConsoleHelper.WriteColored(AppConstants.QUIZ_DESCRIPTION_LABEL, ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine(quiz.Description, ConsoleColors.Default);

        Console.WriteLine();

        ConsoleHelper.WriteColoredLine(AppConstants.USER_INFO_TITLE, ConsoleColors.Title);

        ConsoleHelper.WriteColored(AppConstants.USER_ID_LABEL, ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine(user.Id, ConsoleColors.Default);

        ConsoleHelper.WriteColored(AppConstants.USER_FULLNAME_LABEL, ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"{user.FirstName} {user.LastName}", ConsoleColors.Default);

        ConsoleHelper.WriteColored(AppConstants.USER_USERNAME_LABEL, ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine(user.Username, ConsoleColors.Default);


    }

    public static void DisplayUserInfo(User user)
    {
        ConsoleHelper.WriteColoredLine(AppConstants.USER_INFO_TITLE, ConsoleColors.Title);
        ConsoleHelper.WriteColored(AppConstants.USER_ID_LABEL, ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"{user.Id}", ConsoleColors.Default);
        ConsoleHelper.WriteColored(AppConstants.USER_NAME_LABEL, ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"{user.FirstName} {user.LastName}", ConsoleColors.Default);
        ConsoleHelper.WriteColored(AppConstants.USER_USERNAME_LABEL, ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"{user.Username}", ConsoleColors.Default);
    }

    public static void DisplaySeparator()
    {
        ConsoleHelper.WriteColoredLine(new string('-', 50), ConsoleColors.Default);
    }

    public static void ClearConsole()
    {
        Console.Clear();
    }


}