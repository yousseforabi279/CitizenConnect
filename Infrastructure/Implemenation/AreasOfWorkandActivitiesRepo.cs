using Application.Contracts.Repos;
using Domain.Deputy;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class AreasOfWorkandActivitiesRepo:GenericRepository<AreasOfWorkandActivities>, IAreasOfWorkandActivities
    {
        protected readonly Appcontext _context;
        public AreasOfWorkandActivitiesRepo(Appcontext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<AreasOfWorkandActivities>> GetByDeputyIdAsync(
    int deputyId,
    CancellationToken cancellationToken)
        {
            return await _context.AreasOfWorkAndActivities
                //.Where(x => x.DeputyId == deputyId)
                .ToListAsync(cancellationToken);
        }
    }
}
