using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.ModelView.ModelView;
using TodoApp.ModelView.Request;

namespace TodoApp.Core.Managers.Interfaces
{
    public interface ITodoManager
    {
        TodoResponseView GetTodos(int page = 1, int pageSize = 10, string sortColumn = "",
            string sortDirection = "ascending", string searchText = "");

        TodoModelView GetTodo(int id);
        TodoModelView CreateTodo(UserModelView currentUser, TodoRequest todoRequest);
        TodoModelView PutTodo(UserModelView currentUser, TodoRequest todoRequest);
        TodoModelView AssignTodo(UserModelView currentUser, TodoAssign todoAssign);
        void ArchiveTodo(UserModelView currentUser, int id);
    }
}
