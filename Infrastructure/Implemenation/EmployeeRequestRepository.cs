using Application.Common;
using Application.Contracts.Repos;
using Application.Core.Queries.GetRequestsForEmplyees;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class EmployeeRequestRepository : IEmployeeRequestRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<EmployeeRequestDto>> GetAssignedRequestsAsync(int employeeId, EmployeeRequestFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.CitizenRequirementEmployees
                        .AsNoTracking()
                        .Where(x => x.EmployeeId == employeeId);
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(x =>
                        x.CitizenRequirement.Citizen.FullName
                        .Contains(filter.Name));
            }
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                query = query.Where(x =>
                    x.CitizenRequirement.Citizen.Phone
                        .Contains(filter.Phone));
            }
            if (!string.IsNullOrWhiteSpace(filter.NationalId))
            {
                query = query.Where(x =>
                    x.CitizenRequirement.Citizen.NationalId
                        == filter.NationalId);
            }
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(x =>
                    x.CitizenRequirement.Title
                        .Contains(filter.Title));
            }
            if (filter.Type.HasValue)
            {
                query = query.Where(x =>
                    x.CitizenRequirement.Type ==
                    filter.Type.Value);
            }
            if (filter.Status.HasValue)
            {
                query = query.Where(x =>
                    x.CitizenRequirement.Status ==
                    filter.Status.Value);
            }
            if (filter.Priority.HasValue)
            {
                query = query.Where(x =>
                     x.CitizenRequirement.Priority==filter.Priority.Value);
            }
            var totalCount = await query.CountAsync();
            var items = await query
             .OrderByDescending(x =>
                 x.CitizenRequirement.CreatedAt)
             .Skip(
                 (filter.PageNumber - 1)
                 * filter.PageSize)
             .Take(filter.PageSize)
             .Select(x => new EmployeeRequestDto
             {
                 Id = x.CitizenRequirement.Id,

                 Type = x.CitizenRequirement.Type.ToString(),

                 Title = x.CitizenRequirement.Title,

                 Content = x.CitizenRequirement.Description,

                 CitizenName =
                     x.CitizenRequirement.Citizen.FullName,

                 NationalId =
                     x.CitizenRequirement.Citizen.NationalId,

                 Phone =
                     x.CitizenRequirement.Citizen.Phone,

                 Priority =
                     x.CitizenRequirement.Priority.ToString(),

                 Status =
                     x.CitizenRequirement.Status.ToString(),

                 CreatedAt =
                     x.CitizenRequirement.CreatedAt
             })
             .ToListAsync(cancellationToken);

            return new PaginatedResult<EmployeeRequestDto>
            {
                Items = items,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };

        }
    }
}
