using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.ModelView.ModelView;

namespace TodoApp.Core.Managers.Interfaces
{
    public interface IUserManager : IManager
    {
        UserResponseView GetUsers(int page = 1, int pageSize = 10, string sortColumn = "",
            string sortDirection = "ascending", string searchText = "");
        UserModelView GetUser(int id);
        UserModelView UpdateProfile(UserModelView currentUser, UserModelView request);
        LoginUserResponseView Login(UserLoginView userReg);
        LoginUserResponseView SignUp(UserRegisterView userReg);
        void AssignAdmin(int id);
        void DeleteUser(UserModelView currentUser, int id);
    }
}
