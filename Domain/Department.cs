using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Department
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = null!;

        public ICollection<Employee> Employees {get;}
            = new List<Employee>();
        public ICollection<CitizenRequirement> CitizenRequirements { get;}
            = new List<CitizenRequirement>();



    }   
}
