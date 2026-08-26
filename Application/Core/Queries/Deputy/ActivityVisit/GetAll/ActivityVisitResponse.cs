using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.ActivityVisit.GetAll
{
    public class ActivityVisitResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image_Video { get; set; }
        public string Location { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
