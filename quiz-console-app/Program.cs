using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.Services;
using quiz_console_app.ViewModels;

class Program
{
    static void Main(string[] args)
    {
        List<Question> questions = new QuestionLoader().LoadQuestionsFromJson("software_questions.json");
        List<BookletViewModel> shuffledBooklets = QuizService.GenerateBooklets(questions, 2);
        QuizDisplay.DisplayBooklets(shuffledBooklets);
        Console.ReadLine();
    }
}
