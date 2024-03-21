using QuizAppConsole.ViewModels;

namespace QuizAppConsole.Models;

public class UserAnswerKeyCollection
{
    private Dictionary<Guid, List<UserAnswerKeyViewModel>> _userAnswerKeysByUserId;

    public UserAnswerKeyCollection()
    {
        _userAnswerKeysByUserId = new Dictionary<Guid, List<UserAnswerKeyViewModel>>();
    }

    public void AddUserAnswerKey(Guid userId, List<UserAnswerKeyViewModel> userAnswerKeys)
    {
        _userAnswerKeysByUserId.Add(userId, userAnswerKeys);
    }

    public List<UserAnswerKeyViewModel> GetUserAnswerKeys(Guid userId)
    {
        if (_userAnswerKeysByUserId.ContainsKey(userId))
        {
            return _userAnswerKeysByUserId[userId];
        }
        else
        {
            return new List<UserAnswerKeyViewModel>();
        }
    }
}