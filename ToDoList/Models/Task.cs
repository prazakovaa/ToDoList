using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    internal class TODOTask
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

        
        public int PersonId { get; set; } // cizí klíč -> existuje v tabulce Person
        public Person? Person { get; set; } // víc tasků může mít jedna osoba (n : 1)
    }
}
