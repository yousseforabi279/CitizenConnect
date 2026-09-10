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
    internal class RefreshTokenRepo : GenericRepository<RefreshToken>, IRefreshToken
    {
        protected readonly ApplicationDbContext _context;

        public RefreshTokenRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task RevokeAllForUserAsync(string userId)
        {
            var tokens = await _context.RefreshTokens
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
            }
        }
    }
}
