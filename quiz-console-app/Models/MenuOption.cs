namespace quiz_console_app.Models;

public class MenuOption
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Action Action { get; set; }

    public MenuOption(int id, string name, Action action)
    {
        Id = id;
        Name = name;
        Action = action;
    }
}