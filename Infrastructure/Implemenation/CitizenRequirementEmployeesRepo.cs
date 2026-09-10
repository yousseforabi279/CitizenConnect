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
    internal class CitizenRequirementEmployees: GenericRepository<CitizenRequirementEmployee>, ICitizenRequirementEmployees
    {
        public CitizenRequirementEmployees(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CitizenRequirementEmployee?> GetAssignmentAsync(
            int requirementId,
            int employeeId)
        {
            return await _context.CitizenRequirementEmployees
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.CitizenRequirementId == requirementId &&
                    x.EmployeeId == employeeId);
        }
    }
}
