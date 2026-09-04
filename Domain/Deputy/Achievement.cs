using Domain.Attachment;
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

        // Media metadata
        public string MediaUrl { get; set; }        // full blob URL
        public string MediaFileName { get; set; }    // original file name (for display/download)
        public string BlobName { get; set; }         // the actual name in blob storage (Guid + ext) - needed for delete/update
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
