using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CitizinRequiermentContent
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CitizinRequiermentId { get; set; }

        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public CitizinRequierment CitizinRequierment { get; set; } = null!;

        public Employee Employee { get; set; } = null!;

    }
}
