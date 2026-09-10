using Application.Contracts.Repos;
using Domain;
using Domain.Deputy;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class DeputyRepo : GenericRepository<Deputy>, IDeputy
    {
        public DeputyRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Deputy?> GetDeputyInfo()
        {
            return await _context.Deputies
                .AsNoTracking()
                .OrderByDescending(d => d.Id)
                .FirstOrDefaultAsync();
        }
    }
}
