﻿namespace quiz_console_app.ViewModels;

public class AnswerKeyViewModel
{
    public int Id { get; set; }
    public int BookletId { get; set; }
    public int QuestionId { get; set; }
    public string CorrectOptionText { get; set; }
}
