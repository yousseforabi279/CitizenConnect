using Application.Contracts.Repos;
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
    internal class ActivitiesAndVisitsRepo:GenericRepository<ActivitiesAndVisits>, IActivitiesAndVisits
    {
        protected readonly ApplicationDbContext _context;

        public ActivitiesAndVisitsRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<ActivitiesAndVisits>> GetByDeputyIdAsync(
                    int deputyId,
                    CancellationToken cancellationToken)
        {
            return await _context.ActivitiesAndVisits
                //.Where(x => x.DeputyId == deputyId)
                .OrderByDescending(x => x.Date)
                .ToListAsync(cancellationToken);
        }
    }
}
