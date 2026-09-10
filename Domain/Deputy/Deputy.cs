using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Deputy
{
    public class Deputy
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string FullName { get; set; } = null!;

        public DateOnly BirthDate { get; set; }

        [MaxLength(20)]
        public string PrimaryPhone { get; set; } = null!;

        [MaxLength(20)]
        public string SecondaryPhone { get; set; } = null!;

        [MaxLength(300)]
        public string? Address { get; set; }

        [MaxLength(200)]
        public string? Title { get; set; }

        [MaxLength(4000)]
        public string? Bio { get; set; }

        [MaxLength(4000)]
        public string? AboutPart1 { get; set; }

        [MaxLength(4000)]
        public string? AboutPart2 { get; set; }

        [MaxLength(300)]
        public string? OfficeLocation { get; set; }

        [MaxLength(20)]
        public string? WhatsApp { get; set; }

        [MaxLength(300)]
        public string? FacebookLink { get; set; }

        [MaxLength(500)]
        public string? LocationURL { get; set; }

        [MaxLength(200)]
        public string? Circle { get; set; }

        [MaxLength(200)]
        public string? Appointment { get; set; }


        // Media metadata (replaces raw Video_image string)
        [MaxLength(300)]
        public string BlobName { get; set; }

        [MaxLength(300)]
        public string MediaFileName { get; set; }

        [MaxLength(100)]
        public string ContentType { get; set; }
        public long FileSizeBytes { get; set; }
        public MediaType MediaType { get; set; }
        public DateTime UploadedAt { get; set; }

        [MaxLength(500)]
        public string MediaUrl { get; set; }        // full blob URL


    }
}
