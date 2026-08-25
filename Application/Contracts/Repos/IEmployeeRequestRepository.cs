using Application.Common;
using Application.Core.Queries.GetRequestsForEmplyees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface IEmployeeRequestRepository
    {
        Task<PaginatedResult<EmployeeRequestDto>> GetAssignedRequestsAsync(
          int employeeId,
          EmployeeRequestFilter filter,
          CancellationToken cancellationToken);
    }
}
