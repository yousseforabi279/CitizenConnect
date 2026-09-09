using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Commands.NewFolder;
using Domain;
using MediatR;

namespace Application.Core.Commands.ChangeRequestStatus
{
    public class ChangeRequestStatusCommandHandler
        : IRequestHandler<ChangeRequestStatusCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public ChangeRequestStatusCommandHandler(
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<string>> Handle(
            ChangeRequestStatusCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Get the request
            var citizenRequest =
                await _unitOfWork.CitizinRequierment
                    .GetByIdAsync(request.CitizinRequiermentId);

            if (citizenRequest == null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Citizen request not found.");
            }

            // 2. Get employee from current logged-in user
            var employee =
                await _unitOfWork.Employee
                    .GetByUserIdAsync(_currentUser.UserId);

            if (employee == null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Employee not found.");
            }

            // 3. Check that this employee is assigned to the request
            var assignment =
                citizenRequest.Employees
                    .FirstOrDefault(x => x.EmployeeId == employee.Id);

            if (assignment == null)
            {
                return Result<string>.Failure(
                    ResultStatus.Unauthorized,
                    "You are not assigned to this request.");
            }

            // 4. Change status
            if (request.Status!=null)
                citizenRequest.Status = request.Status.Value;
            if (request.Priority != null)
                citizenRequest.Priority = request.Priority.Value;

            // 5. Save
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(
                "Request status updated successfully.");
        }
    }
}