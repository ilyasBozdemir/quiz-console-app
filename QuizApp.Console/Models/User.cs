namespace QuizAppConsole.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<UserQuiz> UserQuizzes { get; set; }

    public User(string firstName, string lastName, string username)
    {
        UserQuizzes = new List<UserQuiz>();
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        UserQuizzes = new List<UserQuiz>();
    }
}
