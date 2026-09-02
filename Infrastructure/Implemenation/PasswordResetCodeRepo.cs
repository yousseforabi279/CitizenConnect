using Application.Contracts.Repos;
using Domain;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class PasswordResetCodeRepo:GenericRepository<PasswordResetCode>,IPasswordResetCode
    {
        protected readonly Appcontext _context;

        public PasswordResetCodeRepo(Appcontext context) : base(context)
        {
            _context = context;
        }
        public async Task<PasswordResetCode?> GetLatestValidAsync(string userId)
        {
            return await _context.passwordResetCodes
                .Where(c => c.UserId == userId
                         && !c.IsUsed
                         && c.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(c => c.CreatedAt)
                .FirstOrDefaultAsync();
        }
        public async Task InvalidateAllForUserAsync(string userId)
        {
            var codes = await _context.passwordResetCodes
                .Where(c => c.UserId == userId && !c.IsUsed)
                .ToListAsync();

            foreach (var c in codes) c.IsUsed = true;
        }
    }
}
