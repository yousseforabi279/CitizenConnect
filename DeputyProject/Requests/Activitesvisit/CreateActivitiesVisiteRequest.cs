using System.ComponentModel.DataAnnotations;

namespace DeputyProject.Requests.Activitesvisit
{
    public class CreateActivitiesVisiteRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Media { get; set; }

        [Required]
        public string Location { get; set; }

        public DateTime Date { get; set; }
    }
}
