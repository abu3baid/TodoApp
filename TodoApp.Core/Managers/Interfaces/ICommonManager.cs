using TodoApp.ModelView.ModelView;

namespace TodoApp.Core.Managers.Interfaces
{
    public interface ICommonManager : IManager
    {
        UserModelView GetUserRole(UserModelView user);
    }
}
