using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Deputy
{
    public class Achievement
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(4000)]
        public string Description { get; set; }

        // Media metadata
        [MaxLength(500)]
        public string MediaUrl { get; set; }        // full blob URL

        [MaxLength(300)]
        public string MediaFileName { get; set; }    // original file name (for display/download)

        [MaxLength(300)]
        public string BlobName { get; set; }         // the actual name in blob storage (Guid + ext) - needed for delete/update

        [MaxLength(100)]
        public string ContentType { get; set; }       // e.g. image/png, video/mp4
        public long FileSizeBytes { get; set; }
        public MediaType MediaType { get; set; }      // enum: Image or Video
        public DateTime UploadedAt { get; set; }

    }
    public enum MediaType
    {
        Image,
        Video
    }
}
