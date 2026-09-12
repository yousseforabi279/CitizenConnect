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
            var totalCount = await query.CountAsync(cancellationToken);

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

                    Citizen = new CitizenInfoDto
                    {
                        NationalId =
                            x.CitizinRequierment.Citizen.NationalId,

                        FullName =
                            x.CitizinRequierment.Citizen.FullName,

                        Phone =
                            x.CitizinRequierment.Citizen.Phone
                    },

                    Request = new RequestInfoDto
                    {
                        Type = x.CitizinRequierment.Type,

                        Title = x.CitizinRequierment.Title,

                        Description = x.CitizinRequierment.Description,

                        Status = x.CitizinRequierment.Status,

                        Priority = x.CitizinRequierment.Priority,

                        CreatedAt = x.CitizinRequierment.CreatedAt
                    },

                    Media = new MediaInfoDto
                    {
                        BlobName = x.CitizinRequierment.BlobName,

                        FileName = x.CitizinRequierment.MediaFileName,

                        ContentType = x.CitizinRequierment.ContentType
                    },

                    Comments = x.CitizinRequierment.Comments
                        .OrderByDescending(c => c.CreatedAt)
                        .Select(c => new CommentDto
                        {
                            Id = c.Id,
                            Comment = c.Comment,
                            CreatedAt = c.CreatedAt,
                            EmployeeId = c.EmployeeId,
                            EmployeeName = c.Employee.User.FullName ?? ""
                        })
                        .ToList(),
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
