using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Deputy
{
    public class DeputyWords
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string? Title { get; set; }

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
    }
}
