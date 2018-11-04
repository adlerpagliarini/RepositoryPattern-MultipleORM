using System;

namespace Domain.Entities
{
    public class ToDoList
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime TimeToStart { get; set; }

        public DateTime TimeToEnd { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
