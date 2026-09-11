using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.storage;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetRequestsForEmplyees
{
    internal class GetEmployeeComplaintsQueryHandler : IRequestHandler<GetEmployeeRequestsQuery, Result<PaginatedResult<EmployeeRequestDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "request-files";

        public GetEmployeeComplaintsQueryHandler(
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _fileStorageService = fileStorageService;
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

            var employee = await _unitOfWork.Employee.GetByUserIdAsync(userId);
            if (employee == null)
            {
                return Result<PaginatedResult<EmployeeRequestDto>>.Failure(
                    ResultStatus.NotFound,
                    "Employee not found.");
            }

            var filter = new EmployeeRequestFilter
            {
                Type = request.Type,
                Status = request.Status,
                Priority = request.Priority,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _unitOfWork.EmployeeRequestRepository
                .GetAssignedRequestsAsync(employee.Id, filter, cancellationToken);

            foreach (var item in result.Items)
            {
                if (item.Media is not null && !string.IsNullOrEmpty(item.Media.BlobName))
                {
                    item.Media.MediaUrl = _fileStorageService.GetFileUrl(item.Media.BlobName, FolderName);
                }
            }

            return Result<PaginatedResult<EmployeeRequestDto>>.Success(result);
        }
    }
}