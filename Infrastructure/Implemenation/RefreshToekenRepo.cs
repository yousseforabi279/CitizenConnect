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
    internal class RefreshToekenRepo : GenericRepository<RefreshToken>, IRefreshToken
    {
        protected readonly Appcontext _context;

        public RefreshToekenRepo(Appcontext context) : base(context)
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
