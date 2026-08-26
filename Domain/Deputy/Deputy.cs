using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Deputy
{
    public class Deputy
    {
        public int Id { get; set; }

        //public string UserId { get; set; } = null!;

        //public User User { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public DateOnly BirthOfdate { get; set; }
        public string PrimaryPhone { get; set; } = null!;
        public string SecondaryPhone { get; set; } = null!;

        public string? Address { get; set; }
        public string? Title { get; set; }
        public string? Bio { get; set; }
        public string? AboutPart1 { get; set; }
        public string? AboutPart2 { get; set; }
        public string? OfficeLocation { get; set; }
        public string? WhatsApp { get; set; }
        public string? FacebookLing { get; set; }
        public string? LocationURL { get; set; }
        public string? Circle { get; set; }
        public string? Appointment { get; set; }
        public string Image { get; set; }
        public ICollection<DeputyWords> Words { get; set; } // أحدث كلمات النائب عن مشاكل الدائرة
       = new List<DeputyWords>();
        public ICollection<Achievement> Achievement { get; set; } // ما تم انجازه
       = new List<Achievement>();
        public ICollection<ActitvitiesAndVisits> ActitvitiesAndVisits { get; set; } // أحدث الزيارات والفعاليات
       = new List<ActitvitiesAndVisits>();
        public ICollection<AreasOfWorkandActivities> AreasOfWorkandActivities { get; set; } // مجالات العمل والأنشطة
       = new List<AreasOfWorkandActivities>();
        public ICollection<MotionsForInformation> MotionsForInformation { get; set; } // طلبات الإحاطة
       = new List<MotionsForInformation>();

    }
}
