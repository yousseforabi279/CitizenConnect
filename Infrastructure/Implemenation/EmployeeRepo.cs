using Application.Contracts.Repos;
using Domain;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class EmployeeRepo : GenericRepository<Employee>, IEmployee
    {
        protected readonly Appcontext _context;

        public EmployeeRepo(Appcontext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAvailableEmployeesAsync(int departmentId, int organizationId)
        {
            return await _context.Employees.Include(e => e.EmployeeOrganizations)
                .Where(e =>
                        e.IsActive &&
                        e.DepartmentId == departmentId &&
                        e.EmployeeOrganizations
                                .Any(eo => eo.OrganizationId == organizationId))
                .ToListAsync();
        }

        public async Task<Employee?> GetByUserIdAsync(string userId)
        {
            return await _context.Employees
                   .FirstOrDefaultAsync(
                       e => e.UserId == userId);
        }
    }
}
