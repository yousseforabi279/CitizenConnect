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
    public class CitizenRequirementRepo : GenericRepository<CitizenRequirement>, ICitizenRequirement
    {
        public CitizenRequirementRepo(ApplicationDbContext context) : base(context) { }
     
            public async Task<CitizenRequirement?> GetByIdWithDetailsAsync(int id)
        {
            // AsNoTracking is safe here even for the delete caller: DbSet.Remove()
            // auto-attaches an untracked entity (with its key already populated)
            // in the Deleted state.
            return await _context.CitizenRequirements
                .AsNoTracking()
                .Include(x => x.Citizen)
                .Include(x => x.Employees)
                    .ThenInclude(x => x.Employee)
                    .ThenInclude(ww=>ww.User)
                .Include(x => x.Comments)
                     .ThenInclude(x => x.Employee)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}


