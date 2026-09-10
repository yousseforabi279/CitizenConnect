using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PasswordResetCode
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        [MaxLength(450)]
        public string UserId { get; set; } = null!;

        [MaxLength(100)]
        public string CodeHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; } = false;

        public User User { get; set; } = null!;
    }
}
