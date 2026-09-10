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
    internal class AreasOfWorkAndActivitiesRepo:GenericRepository<AreasOfWorkAndActivities>, IAreasOfWorkAndActivities
    {
        protected readonly ApplicationDbContext _context;
        public AreasOfWorkAndActivitiesRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<AreasOfWorkAndActivities>> GetByDeputyIdAsync(
    int deputyId,
    CancellationToken cancellationToken)
        {
            return await _context.AreasOfWorkAndActivities
                //.Where(x => x.DeputyId == deputyId)
                .ToListAsync(cancellationToken);
        }
    }
}
