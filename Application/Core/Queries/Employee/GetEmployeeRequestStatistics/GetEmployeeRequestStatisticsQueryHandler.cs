using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Queries.Employee.GetEmployeeRequestStatistics;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetEmployeeRequestStatistics
{
    public class GetEmployeeRequestStatisticsQueryHandler
        : IRequestHandler<GetEmployeeRequestStatisticsQuery, Result<EmployeeRequestStatisticsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public GetEmployeeRequestStatisticsQueryHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<EmployeeRequestStatisticsDto>> Handle(
            GetEmployeeRequestStatisticsQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
            {
                return Result<EmployeeRequestStatisticsDto>.Failure(
                    ResultStatus.Unauthorized, "User is not authenticated.");
            }

            var userId = _currentUser.UserId;
            if (userId == null)
            {
                return Result<EmployeeRequestStatisticsDto>.Failure(
                    ResultStatus.Unauthorized, "User id not found.");
            }

            var employee = await _unitOfWork.Employee.GetByUserIdAsync(userId);
            if (employee == null)
            {
                return Result<EmployeeRequestStatisticsDto>.Failure(
                    ResultStatus.NotFound, "Employee not found.");
            }

            var stats = await _unitOfWork.Employee
                .GetStatisticsAsync(employee.Id, cancellationToken);

            return Result<EmployeeRequestStatisticsDto>.Success(stats);
        }
    }
}