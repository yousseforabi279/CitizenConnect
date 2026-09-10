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
        protected readonly ApplicationDbContext _context;

        public DeputyRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Deputy?> GetDeputyInfo()
        {
            return await _context.Deputies .OrderByDescending(d => d.Id) // or whatever makes sense
        .FirstOrDefaultAsync();
        }
    }
}
