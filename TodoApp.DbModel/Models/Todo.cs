using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TodoApp.DbModel.Models
{
    public partial class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public int CreatorId { get; set; }
        public int AssignedId { get; set; }
        [Timestamp]
        public DateTime CreatedDateUtcTime { get; set; }
        [Timestamp]
        public DateTime UpdatedDateUtcTime { get; set; }
        public bool IsRead { get; set; }
        public bool IsArchived { get; set; }
        public virtual User Creator { get; set; }
    }
}
