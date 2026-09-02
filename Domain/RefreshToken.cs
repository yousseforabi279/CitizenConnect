using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? RevokedAt { get; set; }
        public bool IsRevoked { get; set; } = false;
        public User User { get; set; } = null!;

    }
}
