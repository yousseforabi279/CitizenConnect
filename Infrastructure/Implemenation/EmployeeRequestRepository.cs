using Application.Common;
using Application.Contracts.Repos;
using Application.Core.Queries.GetRequestsForEmplyees;
using Infrastructure.Dbcontext;
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
        private readonly Appcontext _context;
        public EmployeeRequestRepository(Appcontext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<EmployeeRequestDto>> GetAssignedRequestsAsync(int employeeId, EmployeeRequestFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.CitizinRequiermentEmployees
                        .AsNoTracking()
                        .Where(x => x.EmployeeId == employeeId);
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(x =>
                        x.CitizinRequierment.Citizen.FullName
                        .Contains(filter.Name));
            }
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                query = query.Where(x =>
                    x.CitizinRequierment.Citizen.Phone
                        .Contains(filter.Phone));
            }
            if (!string.IsNullOrWhiteSpace(filter.NationalId))
            {
                query = query.Where(x =>
                    x.CitizinRequierment.Citizen.NationalId
                        == filter.NationalId);
            }
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(x =>
                    x.CitizinRequierment.Title
                        .Contains(filter.Title));
            }
            if (filter.Type.HasValue)
            {
                query = query.Where(x =>
                    x.CitizinRequierment.Type ==
                    filter.Type.Value);
            }
            if (filter.Status.HasValue)
            {
                query = query.Where(x =>
                    x.CitizinRequierment.Status ==
                    filter.Status.Value);
            }
            if (filter.Priority.HasValue)
            {
                query = query.Where(x =>
                     x.CitizinRequierment.Priority==filter.Priority.Value);
            }
            var totalCount = await query.CountAsync();
            var items = await query
             .OrderByDescending(x =>
                 x.CitizinRequierment.CreatedAt)
             .Skip(
                 (filter.PageNumber - 1)
                 * filter.PageSize)
             .Take(filter.PageSize)
             .Select(x => new EmployeeRequestDto
             {
                 Id = x.CitizinRequierment.Id,

                 Type = x.CitizinRequierment.Type.ToString(),

                 Title = x.CitizinRequierment.Title,

                 Content = x.CitizinRequierment.Description,

                 CitizenName =
                     x.CitizinRequierment.Citizen.FullName,

                 NationalId =
                     x.CitizinRequierment.Citizen.NationalId,

                 Phone =
                     x.CitizinRequierment.Citizen.Phone,

                 Priority =
                     x.CitizinRequierment.Priority.ToString(),

                 Status =
                     x.CitizinRequierment.Status.ToString(),

                 CreatedAt =
                     x.CitizinRequierment.CreatedAt
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
