using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TodoApp.Core.Managers.Interfaces;
using TodoApp.DbModel;
using TodoApp.ModelView.ModelView;

namespace TodoApp.Core.Managers
{
    public class RoleManager : IRoleManager
    {
        private tododbContext _tododbContext;
        private IMapper _mapper;

        public RoleManager(tododbContext tododbContext, IMapper mapper)
        {
            _tododbContext = tododbContext;
            _mapper = mapper;
        }

        public bool CheckAccess(UserModelView userModel)
        {
            var isAdmin = _tododbContext.Users.Any(a => a.Id == userModel.Id 
                                                        && a.IsAdmin);
            return isAdmin;
        }
    }
}
