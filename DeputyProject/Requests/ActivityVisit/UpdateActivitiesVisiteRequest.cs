using System.ComponentModel.DataAnnotations;

namespace DeputyProject.Requests.ActivityVisit
{
    public class UpdateActivitiesVisiteRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Media { get; set; } // null = keep existing media

        [Required]
        public string Location { get; set; }

        public DateTime Date { get; set; }
    }
}
