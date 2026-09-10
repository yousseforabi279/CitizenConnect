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
        protected readonly ApplicationDbContext _context;
        public CitizenRequirementEmployees(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<CitizenRequirementEmployee?> GetAssignmentAsync(
    int requirementId,
    int employeeId)
        {
            return await _context.CitizenRequirementEmployees
                .FirstOrDefaultAsync(x =>
                    x.CitizenRequirementId == requirementId &&
                    x.EmployeeId == employeeId);
        }
    }
}
