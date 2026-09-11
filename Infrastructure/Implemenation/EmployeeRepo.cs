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

        public async Task<IEnumerable<Employee>> GetAllwithUserAsync()
        {
            return await _context.Employees
                .Include(x => x.User)
                .ToListAsync();
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
                   .Include(x => x.User)
                    .Include(x => x.Department)
                    .Include(x => x.EmployeeOrganizations)
                        .ThenInclude(x => x.Organization)
                    .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<EmplyeeInfo?> GetEmplyeeInfo(string userId)
        {
            return await _context.Employees
                       .Where(e => e.UserId == userId)
                       .Select(e => new EmplyeeInfo
                       {
                           Name = e.User.FullName,               // adjust to your actual User property
                           Department = e.Department.Name,
                           ImageUrl = e.Image != null ? e.Image.MediaUrl : null,
                           Organizations = e.EmployeeOrganizations
                               .Select(eo => eo.Organization.Name)
                               .ToList()
                       })
       .FirstOrDefaultAsync();
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
        public async Task<Employee?> GetEmpwithitsdata(int employeeId)
        {
            return await _context.Employees.Include(ww => ww.Image)
                        .Include(ww => ww.EmployeeOrganizations)
                                .Include(ww => ww.Department)
                                .FirstOrDefaultAsync(ww => ww.Id == employeeId);
        }
    }
}
