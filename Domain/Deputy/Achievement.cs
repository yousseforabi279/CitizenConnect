using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Deputy
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        // Foreign Key
        public int DeputyId { get; set; }

        // Navigation Property
        public Deputy Deputy { get; set; } = null!;
    }
}
