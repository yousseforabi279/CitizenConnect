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
            return await _context.CitizenRequirements
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


