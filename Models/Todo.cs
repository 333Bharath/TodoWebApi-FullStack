using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoAPI.Models
{
    [Table("Todos")]
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string TodoData { get; set; }
        public bool IsCompleted { get; set; }
    }
}