using Application.Common;
using Application.Contracts.Repos;
using Application.Core.Queries.GetRequestsForDeputy;
using Domain;
using Domain.Deputy;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class DeputyRepo : GenericRepository<Deputy>, IDeputy
    {
        protected readonly Appcontext _context;

        public DeputyRepo(Appcontext context) : base(context)
        {
            _context = context;
        }

        public async Task<Deputy?> GetDeputyInfo()
        {
            return await _context.Deputies .OrderByDescending(d => d.Id) // or whatever makes sense
        .FirstOrDefaultAsync();
        }
        public async Task<PaginatedResult<DeputyRequestDto>> GetAllForDeputyAsync(
    DeputyRequestFilter filter,
    CancellationToken cancellationToken)
        {
            var query = _context.CitizinRequierments
                .Include(r => r.Citizen)
                .Include(r => r.Employees)
                    .ThenInclude(e => e.Employee)
                        .ThenInclude(e => e.User)
                .Include(r => r.Employees)
                    .ThenInclude(e => e.Employee)
                        .ThenInclude(e => e.Department)
                .Include(r => r.Employees)
                    .ThenInclude(e => e.Employee)
                        .ThenInclude(e => e.EmployeeOrganizations)
                            .ThenInclude(eo => eo.Organization)
                .AsQueryable();

            if (filter.Type.HasValue)
                query = query.Where(r => r.Type == filter.Type.Value);

            if (filter.Status.HasValue)
                query = query.Where(r => r.Status == filter.Status.Value);

            if (filter.Priority.HasValue)
                query = query.Where(r => r.Priority == filter.Priority.Value);

            if (!string.IsNullOrWhiteSpace(filter.NationalId))
                query = query.Where(r => r.CitizenNationalId.Contains(filter.NationalId));

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(r => r.Citizen.FullName.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.Phone))
                query = query.Where(r => r.Citizen.Phone.Contains(filter.Phone));

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(r => r.Title.Contains(filter.Title));

            if (filter.DepartmentId.HasValue)
                query = query.Where(r => r.Employees.Any(e => e.Employee.DepartmentId == filter.DepartmentId.Value));

            if (filter.OrganizationId.HasValue)
                query = query.Where(r => r.Employees.Any(e =>
                    e.Employee.EmployeeOrganizations.Any(eo => eo.OrganizationId == filter.OrganizationId.Value)));

            query = query.OrderByDescending(r => r.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(r => new DeputyRequestDto
                {
                    Id = r.Id,
                    Citizen = new CitizenInfoDto
                    {
                        NationalId = r.CitizenNationalId,
                        FullName = r.Citizen.FullName,
                        Phone = r.Citizen.Phone
                    },
                    Request = new RequestInfoDto
                    {
                        Type = r.Type,
                        Title = r.Title,
                        Description = r.Description,
                        Status = r.Status,
                        Priority = r.Priority,
                        CreatedAt = r.CreatedAt
                    },
                    Media = r.BlobName != null ? new MediaInfoDto
                    {
                        BlobName = r.BlobName,
                        FileName = r.MediaFileName,
                        ContentType = r.ContentType,
                        MediaUrl = null
                    } : null,
                    AssignedEmployees = r.Employees.Select(e => new AssignedEmployeeDto
                    {
                        EmployeeId = e.Employee.Id,
                        FullName = e.Employee.User.FullName,
                        DepartmentName = e.Employee.Department.Name,
                        Organizations = e.Employee.EmployeeOrganizations
                            .Select(eo => eo.Organization.Name)
                            .ToList()
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return new PaginatedResult<DeputyRequestDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }
    }
}
