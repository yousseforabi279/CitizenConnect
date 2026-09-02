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
        public string Image { get; set; }
        public ICollection<AchievementAttachment> Attachments { get; set; }
        = new List<AchievementAttachment>();
    }
}
