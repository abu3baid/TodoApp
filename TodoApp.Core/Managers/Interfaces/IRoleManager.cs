using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.ModelView.ModelView;

namespace TodoApp.Core.Managers.Interfaces
{
    public interface IRoleManager : IManager
    {
        bool CheckAccess(UserModelView userModelView);
    }
}
