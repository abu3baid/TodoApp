using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.ModelView.ModelView
{
    public class UserModelView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [DefaultValue("")]
        public string Image { get; set; }
        public string ImageString { get; set; }
        public bool IsAdmin { get; set; }
    }
}
