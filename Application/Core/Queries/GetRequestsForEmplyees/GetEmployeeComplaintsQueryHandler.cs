using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetRequestsForEmplyees
{
    internal class GetEmployeeComplaintsQueryHandler : IRequestHandler<GetEmployeeRequestsQuery, Result<PaginatedResult<EmployeeRequestDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public GetEmployeeComplaintsQueryHandler(
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<PaginatedResult<EmployeeRequestDto>>> Handle(GetEmployeeRequestsQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
            {
                return Result<PaginatedResult<EmployeeRequestDto>>.Failure(
                    ResultStatus.Unauthorized,
                    "User is not authenticated.");
            }
            var userId = _currentUser.UserId;

            if (userId == null)
            {
                return Result<PaginatedResult<EmployeeRequestDto>>.Failure(
                    ResultStatus.Unauthorized,
                    "User id not found.");
            }
            var employee =
                   await _unitOfWork.Employee
                       .GetByUserIdAsync(userId);
            if (employee == null)
            {
                return Result<PaginatedResult<EmployeeRequestDto>>
                    .Failure(
                        ResultStatus.NotFound,
                        "Employee not found.");
            }
            var requestType = request.Type ?? RequestType.Complaint;

            var filter = new EmployeeRequestFilter
            {
                Type = request.Type,
                Status = request.Status,
                Priority = request.Priority,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            var result =
                await _unitOfWork.EmployeeRequestRepository
                    .GetAssignedRequestsAsync(
                        employee.Id,
                        filter,
                        cancellationToken);

            return Result<PaginatedResult<EmployeeRequestDto>>
                        .Success(result);

        }
    }
}
