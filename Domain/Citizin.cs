using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Citizen
    {
        [Key]
        public string NationalId { get; set; } = null!;
        public string FullName { get; set; } = null!;

        public DateOnly BirthDate { get; set; }

        public string Phone { get; set; } = null!;

        public ICollection<CitizinRequierment> Requests { get; set; } = new List<CitizinRequierment>();
    }
}
