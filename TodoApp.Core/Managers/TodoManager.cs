using System;
using System.Linq;
using AutoMapper;
using TodoApp.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using TodoApp.Common.Extceptions;
using TodoApp.Core.Managers.Interfaces;
using TodoApp.DbModel;
using TodoApp.DbModel.Models;
using TodoApp.ModelView.ModelView;
using TodoApp.ModelView.Request;
using TodoApp.Common.Helper;

namespace TodoApp.Core.Managers
{
    public class TodoManager : ITodoManager
    {
        private tododbContext _tododbContext;
        private IMapper _mapper;

        public TodoManager(tododbContext tododbContext, IMapper mapper)
        {
            _tododbContext = tododbContext;
            _mapper = mapper;
        }
        public TodoModelView GetTodo(int id)
        {
            var res = _tododbContext.Todos
                          .Include("Creator")
                          .FirstOrDefault(a => a.Id == id)
                      ?? throw new ServiceValidationException("Invalid todo id received");

            return _mapper.Map<TodoModelView>(res);
        }

        public TodoResponseView GetTodos(int page = 1, int pageSize = 10, string sortColumn = "", string sortDirection = "ascending", string searchText = "")
        {
            var queryRes = _tododbContext.Todos
                                        .Where(a => string.IsNullOrWhiteSpace(searchText)
                                                    || (a.Title.Contains(searchText)
                                                        || a.Content.Contains(searchText)));

            if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("ascending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderBy(sortColumn);
            }
            else if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("descending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderByDescending(sortColumn);
            }

            var res = queryRes.GetPaged(page, pageSize);

            var userIds = res.Data
                             .Select(a => a.CreatorId)
                             .Distinct()
                             .ToList();

            var users = _tododbContext.Users
                                     .Where(a => userIds.Contains(a.Id))
                                     .ToDictionary(a => a.Id, x => _mapper.Map<UserResultView>(x));

            var data = new TodoResponseView()
            {
                Todo = _mapper.Map<PagedResult<TodoModelView>>(res),
                User = users
            };

            data.Todo.Sortable.Add("Title", "Title");
            data.Todo.Sortable.Add("CreatedDate", "Created Date");

            return data;
        }

        public TodoModelView CreateTodo(UserModelView currentUser, TodoRequest todoRequest)
        {
            var todo = _tododbContext.Todos.Add(new Todo
            {
                Title = todoRequest.Title,
                Image = todoRequest.Image,
                Content = todoRequest.Content,
                CreatorId = currentUser.Id,
                AssignedId = currentUser.Id,
            }).Entity;

            _tododbContext.SaveChanges();
            return _mapper.Map<TodoModelView>(todo);
        }

        public TodoModelView PutTodo(UserModelView currentUser, TodoRequest todoRequest)
        {
            var assignedId = _tododbContext.Todos.FirstOrDefault(a=>a.Id==todoRequest.Id);

            if (!currentUser.IsAdmin && assignedId.AssignedId != currentUser.Id)
            {
                throw new ServiceValidationException("You dont have permission to edit this todo");
            }

            var todo = _tododbContext.Todos
                   .FirstOrDefault(a => a.Id == todoRequest.Id)
               ?? throw new ServiceValidationException("Invalid todo id received");

            var url = "";

            if (!string.IsNullOrWhiteSpace(todoRequest.ImageString))
            {
                url = Helper.SaveImage(todoRequest.ImageString, "todoimages");
            }
            todo.Title = todoRequest.Title;
            todo.Content = todoRequest.Content;
            todo.IsRead = todoRequest.IsRead;
            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseUrl = "https://localhost:44380";
                todo.Image = $@"{baseUrl}/api/v1/user/fileretrive/todopic?filename={url}";
            }

            _tododbContext.SaveChanges();
            return _mapper.Map<TodoModelView>(todo);
        }

        public TodoModelView AssignTodo(UserModelView currentUser, TodoAssign todoAssign)
        {
            _tododbContext.IgnoreFilter = true;
            var todo = _tododbContext.Todos
                           .FirstOrDefault(a => a.Id == todoAssign.Id)
                       ?? throw new ServiceValidationException("Invalid todo id received");
            todo.AssignedId = todoAssign.AssignedId;

            _tododbContext.SaveChanges();
            return _mapper.Map<TodoModelView>(todo);
        }

        public void ArchiveTodo(UserModelView currentUser, int id)
        {
            _tododbContext.IgnoreFilter = true;
            var data = _tododbContext.Todos
                           .FirstOrDefault(a => a.Id == id)
                       ?? throw new ServiceValidationException("Invalid todo id received");
            data.IsArchived = true;
            _tododbContext.SaveChanges();
        }
    }
}
