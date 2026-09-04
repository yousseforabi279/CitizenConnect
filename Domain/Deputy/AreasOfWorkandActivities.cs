using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Deputy
{
    public class AreasOfWorkandActivities
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Media metadata (replaces raw Image string)
        public string? BlobName { get; set; }
        public string? MediaFileName { get; set; }
        public string? ContentType { get; set; }
        public long? FileSizeBytes { get; set; }
        public MediaType? MediaType { get; set; }
        public DateTime? UploadedAt { get; set; }
    }
}
