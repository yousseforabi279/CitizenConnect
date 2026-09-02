using Application.Contracts.Repos;
using Application.Core.Queries.Employee.GetEmployeeInfo;
using Application.Core.Queries.Employee.GetEmployeeRequestStatistics;
using Domain;
using Domain.Enums;
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

        public async Task<EmplyeeInfo?> GetEmplyeeInfo(string userId)
        {
            return await _context.Employees.Where(ww => ww.UserId == userId)
                .Select(ww => new EmplyeeInfo { Name = ww.User.FullName,Department = ww.Department.Name }).FirstOrDefaultAsync();
        }
        public async Task<EmployeeRequestStatisticsDto> GetStatisticsAsync(int employeeId, CancellationToken cancellationToken)
        {
            var grouped = await _context.CitizinRequiermentEmployees
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId)
                    .GroupBy(x => x.CitizinRequierment.Status)
                    .Select(g => new { Status = g.Key, Count = g.Count() })
                    .ToListAsync(cancellationToken);

            return new EmployeeRequestStatisticsDto
            {
                Total = grouped.Sum(g => g.Count),
                New = grouped.FirstOrDefault(g => g.Status == RequestStatus.New)?.Count ?? 0,
                InProgress = grouped.FirstOrDefault(g => g.Status == RequestStatus.InProgress)?.Count ?? 0,
                Completed = grouped.FirstOrDefault(g => g.Status == RequestStatus.Resolved)?.Count ?? 0
            };
        }
    }
}
