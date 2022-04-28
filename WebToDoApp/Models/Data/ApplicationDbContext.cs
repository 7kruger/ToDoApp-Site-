using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebToDoApp.Models.Entities;

namespace WebToDoApp.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoItem> ToDoList { get; set; }
    }
}