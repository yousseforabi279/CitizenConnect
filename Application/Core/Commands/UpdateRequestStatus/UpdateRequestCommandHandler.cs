using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Commands.UpdateRequestStatus;
using Domain;
using MediatR;

namespace Application.Core.Commands.UpdateRequest
{
    public class UpdateRequestCommandHandler
        : IRequestHandler<UpdateRequestCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public UpdateRequestCommandHandler(
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<string>> Handle(
            UpdateRequestCommand request,
            CancellationToken cancellationToken)
        {
            var citizenRequest =
                await _unitOfWork.CitizinRequierment
                    .GetByIdAsync(request.CitizinRequiermentId);

            if (citizenRequest == null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Citizen request not found.");
            }

            var employee =
                await _unitOfWork.Employee
                    .GetByUserIdAsync(_currentUser.UserId);

            if (employee == null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Employee not found.");
            }

            var assignment =
     await _unitOfWork.CitizinRequiermentEmployees
         .GetAssignmentAsync(
             citizenRequest.Id,
             employee.Id);


            if (assignment == null)
            {
                return Result<string>.Failure(
                    ResultStatus.Unauthorized,
                    "You are not assigned to this request.");
            }

            if (request.Status.HasValue)
            {
                citizenRequest.Status = request.Status.Value;
            }

            if (request.Priority.HasValue)
            {
                citizenRequest.Priority = request.Priority.Value;
            }

            if (!string.IsNullOrWhiteSpace(request.Comment))
            {
                var comment = new CitizinRequiermentContent
                {
                    CitizinRequiermentId =
                        request.CitizinRequiermentId,

                    EmployeeId = employee.Id,

                    Comment = request.Comment,

                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.CitizinRequiermentContent
                    .AddAsync(comment);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(
                "Request updated successfully.");
        }
    }
}