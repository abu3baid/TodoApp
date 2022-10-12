using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Common.Extensions;

namespace TodoApp.ModelView.ModelView
{
    public class UserResponseView
    {
        public PagedResult<UserModelView> User { get; set; }
        public Dictionary<int, TodoResultView> Todo { get; set; }
    }
}
