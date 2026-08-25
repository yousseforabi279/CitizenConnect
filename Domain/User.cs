using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User: IdentityUser
    {
        public string? FullName { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
       = new List<RefreshToken>();
    }
}
