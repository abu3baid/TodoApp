using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TodoApp.ModelView.ModelView
{
    public class TodoModelView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DefaultValue("")]
        public string Image { get; set; }
        public string Content { get; set; }
        public int CreatorId { get; set; }
        public int AssignedId { get; set; }
        public DateTime CreatedDateUtcTime { get; set; }
    }
}
