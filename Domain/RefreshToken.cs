using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RefreshToken
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string TokenHash { get; set; } = null!;

        [MaxLength(450)]
        public string UserId { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? RevokedAt { get; set; }
        public bool IsRevoked { get; set; } = false;
        public User User { get; set; } = null!;

    }
}
