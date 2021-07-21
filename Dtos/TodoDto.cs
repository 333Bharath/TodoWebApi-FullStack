using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoAPI.Dtos
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string TodoData { get; set; }
        public bool IsCompleted { get; set; }
    }
}