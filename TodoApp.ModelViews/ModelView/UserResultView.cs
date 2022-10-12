using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.ModelView.ModelView
{
    public class UserResultView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [DefaultValue("")]
        public string Image { get; set; }
    }
}