using quiz_console_app.Helpers;
using quiz_console_app.Models;
using quiz_console_app.Services;

QuizService quizService = new QuizService();


List<Question> questions = new QuestionLoader().LoadQuestionsFromJson("software_questions.json"); // default path : {root-project}/bin/Debug/net8.0/software_questions.json

quizService.GenerateBooklets(questions, 1);

QuizDisplay.DisplayBooklets(quizService.Booklets);
QuizDisplay.DisplayAnswerKeys(quizService.AnswerKeys);

Console.ReadLine();