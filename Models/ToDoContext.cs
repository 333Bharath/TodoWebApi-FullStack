using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ToDoAPI.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext() : base("todoConnectionString")
        {

        }

        public DbSet<Todo> Todos { get; set; }

    }
}