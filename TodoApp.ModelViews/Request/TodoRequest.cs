using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.ModelView.Request
{
    public class TodoRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DefaultValue("")]
        public string Image { get; set; }
        public string ImageString { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}
