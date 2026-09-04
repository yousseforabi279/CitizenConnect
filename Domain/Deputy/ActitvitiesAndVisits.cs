using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Deputy
{
    public class ActitvitiesAndVisits
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        // Media metadata (like Achievement)
        public string? BlobName { get; set; }
        public string? MediaFileName { get; set; }
        public string? ContentType { get; set; }
        public long? FileSizeBytes { get; set; }
        public MediaType? MediaType { get; set; }
        public DateTime? UploadedAt { get; set; }

        [Required]
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}
