using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    internal class Person
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TODOTask> Tasks { get; set; } //jeden človek má víc tasků (1 : n)
    }
}
