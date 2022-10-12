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
        UserModelView UpdateProfile(UserModelView currentUser, UserModelView request);
        LoginUserResponseView Login(UserLoginView userReg);

        LoginUserResponseView SignUp(UserRegisterView userReg);

        void DeleteUser(UserModelView currentUser, int id);
    }
}
