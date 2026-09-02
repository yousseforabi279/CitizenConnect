using Domain.Deputy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Attachment
{
    public class AchievementAttachment
    {
        public int Id { get; set; }

        public int AchievementId { get; set; }

        public string FileName { get; set; } = null!;

        public string BlobName { get; set; } = null!;

        public string ContentType { get; set; } = null!;

        public long FileSize { get; set; }

        public DateTime CreatedAt { get; set; }

        public Achievement Achievement { get; set; } = null!;
    }
}
