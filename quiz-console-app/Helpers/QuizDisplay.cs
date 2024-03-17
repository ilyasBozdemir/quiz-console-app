using quiz_console_app.Models;
using quiz_console_app.ViewModels;

namespace quiz_console_app.Helpers;

public class QuizDisplay
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
        ConsoleHelper.WriteColoredLine($"Doğru Sayısı: {resultSummary.CorrectCount}", ConsoleColors.Success);
        ConsoleHelper.WriteColoredLine($"Yanlış Sayısı: {resultSummary.IncorrectCount}", ConsoleColors.Error);
        ConsoleHelper.WriteColoredLine($"Boş Sayısı: {resultSummary.BlankCount}", ConsoleColors.Warning);
        ConsoleHelper.WriteColoredLine($"Net: {resultSummary.NetCount}", ConsoleColors.Default);
        ConsoleHelper.WriteColoredLine($"Başarı Yüzdesi: {resultSummary.SuccessPercentage:F2}%", ConsoleColors.Info);
    }
}