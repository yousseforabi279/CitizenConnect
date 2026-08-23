using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<Employee> Employees {get;}
            = new List<Employee>();
        public ICollection<CitizinRequierment> CitizinRequierments { get;}
            = new List<CitizinRequierment>();



    }   
}
