using Application.Contracts.Repos;
using Domain;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class CitizinRequiermentEmployees: GenericRepository<CitizinRequiermentEmployee>, ICitizinRequiermentEmployees
    {
        protected readonly Appcontext _context;
        public CitizinRequiermentEmployees(Appcontext context) : base(context)
        {
            _context = context;
        }
        public async Task<CitizinRequiermentEmployee?> GetAssignmentAsync(
    int requirementId,
    int employeeId)
        {
            return await _context.CitizinRequiermentEmployees
                .FirstOrDefaultAsync(x =>
                    x.CitizinRequiermentId == requirementId &&
                    x.EmployeeId == employeeId);
        }
    }
}
