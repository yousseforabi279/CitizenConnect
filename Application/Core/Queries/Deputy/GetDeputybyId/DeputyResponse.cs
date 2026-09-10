using Domain.Deputy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.GetDeputybyId
{
    public class DeputyResponse
    {

        public string FullName { get; set; } = null!;

        public DateOnly BirthDate { get; set; }

        public string PrimaryPhone { get; set; } = null!;

        public string? SecondaryPhone { get; set; }

        public string? Address { get; set; }

        public string? Title { get; set; }

        public string? Bio { get; set; }

        public string? AboutPart1 { get; set; }
        public string? AboutPart2 { get; set; }
        public string? OfficeLocation { get; set; }

        public string? WhatsApp { get; set; }

        public string? FacebookLink { get; set; }

        public string? LocationURL { get; set; }

        public string? Circle { get; set; }

        public string? Appointment { get; set; }

        public string MediaUrl { get; set; }
        public string ContentType { get; set; }
        public MediaType? MediaType { get; set; }
    }
}
