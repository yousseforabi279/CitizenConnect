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
    internal class ActitvitiesAndVisitsRepo:GenericRepository<ActitvitiesAndVisits>, IActitvitiesAndVisits
    {
        protected readonly Appcontext _context;

        public ActitvitiesAndVisitsRepo(Appcontext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<ActitvitiesAndVisits>> GetByDeputyIdAsync(
                    int deputyId,
                    CancellationToken cancellationToken)
        {
            return await _context.ActitvitiesAndVisits
                .Where(x => x.DeputyId == deputyId)
                .OrderByDescending(x => x.Date)
                .ToListAsync(cancellationToken);
        }
    }
}
