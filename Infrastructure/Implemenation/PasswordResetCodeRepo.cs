using Application.Contracts.Repos;
using Domain;
using Infrastructure.Data;
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
        public PasswordResetCodeRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PasswordResetCode?> GetLatestValidAsync(string userId)
        {
            return await _context.PasswordResetCodes
                .AsNoTracking()
                .Where(c => c.UserId == userId
                         && !c.IsUsed
                         && c.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(c => c.CreatedAt)
                .FirstOrDefaultAsync();
        }
        public async Task InvalidateAllForUserAsync(string userId)
        {
            await _context.PasswordResetCodes
                .Where(c => c.UserId == userId && !c.IsUsed)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.IsUsed, true));
        }
    }
}
