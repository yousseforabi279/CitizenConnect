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
    internal class CitizenRepo : GenericRepository<Citizen>, ICitizen
    {
        public CitizenRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Citizen?> GetByNationalidAsync(string id)
        {
            // Deliberately tracked (not AsNoTracking): CreateComplaintCommandHandler
            // wires an existing Citizen straight into a new CitizenRequirement's
            // navigation without an explicit Attach/Update, relying on it staying
            // tracked from this query within the same DbContext — detaching it
            // would make EF try to re-insert the citizen and hit a PK conflict.
            return await _context.Citizens
                .SingleOrDefaultAsync(ww=>ww.NationalId == id);
        }
    }
}
