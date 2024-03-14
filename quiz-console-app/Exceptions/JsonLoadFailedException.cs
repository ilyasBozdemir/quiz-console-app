namespace quiz_console_app.Exceptions;

public class JsonLoadFailedException : Exception
{
    public JsonLoadFailedException() : base("JSON dosyası yüklenirken bir hata oluştu.") { }

    public JsonLoadFailedException(string message) : base(message) { }

    public JsonLoadFailedException(string message, Exception innerException) : base(message, innerException) { }
}