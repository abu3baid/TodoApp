using System.Collections.Generic;
using TodoApp.Common.Extensions;

namespace TodoApp.ModelView.ModelView
{
    public class TodoResponseView
    {
        public PagedResult<TodoModelView> Todo { get; set; }
        public Dictionary<int, UserResultView> User { get; set; }
    }
}
