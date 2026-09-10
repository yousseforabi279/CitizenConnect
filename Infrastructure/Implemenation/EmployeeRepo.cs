using Application.Contracts.Repos;
using Application.Core.Queries.Employee.GetEmployeeInfo;
using Application.Core.Queries.Employee.GetEmployeeRequestStatistics;
using Domain;
using Domain.Enums;
using Infrastructure.Data;
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
        public EmployeeRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllwithUserAsync()
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<List<Employee>> GetAvailableEmployeesAsync(int departmentId, int organizationId)
        {
            // Deliberately tracked (not AsNoTracking): CreateComplaintCommandHandler
            // wires each returned Employee straight into new
            // CitizenRequirementEmployee join-entity navigations without an
            // explicit Attach/Update, relying on them staying tracked from this
            // query — detaching them would make EF try to re-insert the
            // employees and hit a PK conflict.
            //
            // Filtering with .Any() on the navigation translates to an EXISTS
            // subquery — no need to eagerly load every employee's full
            // EmployeeOrganizations collection just to check membership.
            return await _context.Employees
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
                   .AsNoTracking()
                   .Include(x => x.User)
                    .Include(x => x.Department)
                    .Include(x => x.EmployeeOrganizations)
                        .ThenInclude(x => x.Organization)
                    .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<EmplyeeInfo?> GetEmployeeInfo(string userId)
        {
            return await _context.Employees.Where(ww => ww.UserId == userId)
                .Select(ww => new EmplyeeInfo { Name = ww.User.FullName,Department = ww.Department.Name }).FirstOrDefaultAsync();
        }
        public async Task<EmployeeRequestStatisticsDto> GetStatisticsAsync(int employeeId, CancellationToken cancellationToken)
        {
            var grouped = await _context.CitizenRequirementEmployees
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId)
                    .GroupBy(x => x.CitizenRequirement.Status)
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
