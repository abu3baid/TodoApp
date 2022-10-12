using System.ComponentModel;

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