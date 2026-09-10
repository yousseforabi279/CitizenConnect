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
        [MaxLength(14)]
        public string NationalId { get; set; } = null!;

        [MaxLength(200)]
        public string FullName { get; set; } = null!;

        public DateOnly BirthDate { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; } = null!;

        public ICollection<CitizenRequirement> Requests { get; set; } = new List<CitizenRequirement>();
    }
}
