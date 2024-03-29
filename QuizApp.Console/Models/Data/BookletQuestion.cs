﻿using QuizAppConsole.Enums;

namespace QuizAppConsole.Models.Data;

public class BookletQuestion
{
    public int Id { get; set; } = 0;
    public string AskText { get; set; } = "";
    public string Explanation { get; set; } = "";
    public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.Easy;
    public List<BookletQuestionOption> QuestionOptions { get; set; } = new List<BookletQuestionOption>();

    public BookletQuestion()
    {
        QuestionOptions = new List<BookletQuestionOption>();
    }
    public bool ValidateCorrectOptionCount()
    {
        int correctCount = QuestionOptions.Count(option => option.IsCorrect);
        return correctCount == 1;
    }

    public bool ValidateIncorrectOptionCount()
    {
        int correctCount = QuestionOptions.Count(option => option.IsCorrect);
        int incorrectCount = QuestionOptions.Count(option => !option.IsCorrect);

        return ValidateIncorrectOptionCount(correctCount, incorrectCount);
    }

    private bool ValidateIncorrectOptionCount(int correctCount, int incorrectCount)
    {
        return incorrectCount == QuestionOptions.Count - 1 && correctCount + incorrectCount == QuestionOptions.Count;
    }
}
