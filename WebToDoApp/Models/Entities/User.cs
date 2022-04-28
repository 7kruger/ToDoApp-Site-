using System.Collections.Generic;

namespace WebToDoApp.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<ToDoItem> ToDoList { get; set; }
        public User()
        {
            ToDoList = new List<ToDoItem>();
        }
    }
}