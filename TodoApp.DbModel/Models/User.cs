using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TodoApp.DbModel.Models
{
    public partial class User
    {
        public User()
        {
            Todos = new HashSet<Todo>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsArchived { get; set; }

        [Timestamp]
        public DateTime CreatedDateUtcTime { get; set; }

        [Timestamp]
        public DateTime UpdatedDateUtcTime { get; set; }
        public virtual ICollection<Todo> Todos { get; set; }
    }
}
