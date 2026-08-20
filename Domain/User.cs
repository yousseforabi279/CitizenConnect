using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User: IdentityUser
    {
        public string NationalId { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
