using QuizAppConsole.Constants;

namespace QuizAppConsole.Exceptions;

public class JsonLoadFailedException : Exception
{
    public JsonLoadFailedException() : base(string.Format(AppConstants.FILE_LOAD_ERROR_MESSAGE_TEMPLATE, AppConstants.JSON_FORMAT_NAME)) { }

    public JsonLoadFailedException(string message) : base(message) { }

    public JsonLoadFailedException(string message, Exception innerException) : base(message, innerException) { }
}