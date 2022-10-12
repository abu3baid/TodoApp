using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.ModelView.ModelView
{
    public class TodoResultView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DefaultValue("")]
        public string Image { get; set; }
        public string Content { get; set; }
        public int CreatorId { get; set; }
        public int AssignedId { get; set; }
    }
}
