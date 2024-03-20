using quiz_console_app.Constants;
using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Services;

public class QuizConsoleDisplayService
{
    public static void DisplayBooklets(List<BookletViewModel> booklets)
    {
        foreach (var booklet in booklets)
        {
            ConsoleHelper.WriteColored("Kitapçık Id: ", ConsoleColors.Info);
            ConsoleHelper.WriteColoredLine(booklet.Id, ConsoleColors.Default);

            ConsoleHelper.WriteColored("Kitapçık Adı: ", ConsoleColors.Info);
            ConsoleHelper.WriteColoredLine(booklet.BookletName, ConsoleColors.Default);

            foreach (var question in booklet.Questions)
                DisplayQuestion(question);

            Console.WriteLine();
        }
    }

    private static void DisplayQuestion(QuestionViewModel question)
    {
        Console.WriteLine();
        ConsoleHelper.WriteColored("Soru : ", ConsoleColors.Prompt);
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
            ConsoleHelper.WriteColoredLine("Cevap anahtarları bulunamadı.", ConsoleColors.Error);
            return;
        }

        ConsoleHelper.WriteColored("Cevap Anahtarları:", ConsoleColors.Info);

        int questionNumber = 1;

        foreach (var answerKey in answerKeys)
        {
            ConsoleHelper.WriteColored("Kitapçık Id: ", ConsoleColors.Info);
            ConsoleHelper.WriteColoredLine(answerKey.BookletId, ConsoleColors.Default);

            ConsoleHelper.WriteColored(" Soru Id: ", ConsoleColors.Info);
            ConsoleHelper.WriteColoredLine(answerKey.QuestionId, ConsoleColors.Default);

            ConsoleHelper.WriteColored(" Doğru Seçenek : ", ConsoleColors.Info);
            ConsoleHelper.WriteColored(answerKey.CorrectOptionText, ConsoleColors.Default);

            ConsoleHelper.WriteColoredLine(answerKey.CorrectOptionText, ConsoleColors.Default);

            questionNumber++;
        }
    }

    public static void EvaluateQuizResults(QuizResultSummary resultSummary)
    {
        Console.WriteLine();
        ConsoleHelper.WriteColoredLine($"Toplam Soru Sayısı: {resultSummary.TotalQuestions}", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"Doğru Sayısı: {resultSummary.CorrectCount}", ConsoleColors.Success);
        ConsoleHelper.WriteColoredLine($"Yanlış Sayısı: {resultSummary.IncorrectCount}", ConsoleColors.Error);
        ConsoleHelper.WriteColoredLine($"Yanlış Cevap Cezası Durumu : {resultSummary.ScoringRules.PenaltyForIncorrectAnswer}", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"Boş Sayısı: {resultSummary.BlankCount}", ConsoleColors.Warning);
        ConsoleHelper.WriteColoredLine($"Net: {resultSummary.NetCount:F2}/{resultSummary.TotalQuestions}", ConsoleColors.Default);
        ConsoleHelper.WriteColoredLine($"Başarı Yüzdesi: {resultSummary.NetCount / resultSummary.TotalQuestions * 100:F2}%", ConsoleColors.Default);
        Console.WriteLine();
    }


    public static void DisplayQuizAndUserData(Quiz quiz, User user)
    {
        ConsoleHelper.WriteColoredLine("Quiz Bilgileri: ", ConsoleColors.Title);

        ConsoleHelper.WriteColored("Oluşturan: ", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine(quiz.Creator, ConsoleColors.Default);

        ConsoleHelper.WriteColored("Başlık: ", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine(quiz.Title, ConsoleColors.Default);

        ConsoleHelper.WriteColored("Açıklama: ", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine(quiz.Description, ConsoleColors.Default);

        Console.WriteLine();

        ConsoleHelper.WriteColoredLine("Kullanıcı Bilgileri: ", ConsoleColors.Title);

        ConsoleHelper.WriteColored("Kullanıcı ID: ", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine(user.Id, ConsoleColors.Default);

        ConsoleHelper.WriteColored("Ad Soyad : ", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"{user.FirstName} {user.LastName}", ConsoleColors.Default);

        ConsoleHelper.WriteColored("Kullanıcı Adı : ", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine(user.Username, ConsoleColors.Default);
    }
    public static void DisplayUserInfo(User user)
    {
        ConsoleHelper.WriteColoredLine("Kullanıcı Bilgileri:", ConsoleColors.Title);
        ConsoleHelper.WriteColored("ID: ", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"{user.Id}", ConsoleColors.Default);
        ConsoleHelper.WriteColored("Ad: ", ConsoleColors.Info);
        ConsoleHelper.WriteColoredLine($"{user.FirstName} {user.LastName}", ConsoleColors.Default);
        ConsoleHelper.WriteColored("Kullanıcı Adı: ", ConsoleColors.Info);
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