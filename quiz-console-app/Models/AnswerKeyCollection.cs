using quiz_console_app.ViewModels;

namespace quiz_console_app.Models;

public class AnswerKeyCollection
{
    private Dictionary<int, List<AnswerKeyViewModel>> _answerKeysByBookletId;

    public AnswerKeyCollection()
    {
        _answerKeysByBookletId = new Dictionary<int, List<AnswerKeyViewModel>>();
    }

    public void AddAnswerKey(int bookletId, List<AnswerKeyViewModel> answerKeys)
    {
        _answerKeysByBookletId.Add(bookletId, answerKeys);
    }

    public List<AnswerKeyViewModel> GetAnswerKeys(int bookletId)
    {
        if (_answerKeysByBookletId.ContainsKey(bookletId))
        {
            return _answerKeysByBookletId[bookletId];
        }
        else
        {
            return new List<AnswerKeyViewModel>();
        }
    }
}

