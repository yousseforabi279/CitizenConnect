using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.storage;
using MediatR;

namespace Application.Core.Commands.DeleteCitizenRequest
{
    public class DeleteCitizenRequestCommandHandler
        : IRequestHandler<DeleteCitizenRequestCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICurrentUser _currentUser;

        private const string FolderName = "request-files";

        public DeleteCitizenRequestCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService,
            ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
            _currentUser = currentUser;
        }

        public async Task<Result<string>> Handle(
            DeleteCitizenRequestCommand request,
            CancellationToken cancellationToken)
        {
            var citizenRequest =
                await _unitOfWork.CitizenRequirement
                    .GetByIdWithDetailsAsync(request.Id);

            if (citizenRequest is null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Citizen request not found.");
            }

            var employee =
                await _unitOfWork.Employee
                    .GetByUserIdAsync(_currentUser.UserId);

            if (employee is null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Employee not found.");
            }

            var assignment =
                await _unitOfWork.CitizenRequirementEmployees
                    .GetAssignmentAsync(citizenRequest.Id, employee.Id);

            if (assignment is null)
            {
                return Result<string>.Failure(
                    ResultStatus.Unauthorized,
                    "You are not assigned to this request.");
            }

            // Delete employees relations
            foreach (var assignedEmployee in citizenRequest.Employees)
            {
                _unitOfWork.CitizenRequirementEmployees
                    .Delete(assignedEmployee);
            }

            // Delete comments
            foreach (var comment in citizenRequest.Comments)
            {
                _unitOfWork.CitizenRequirementContent
                    .Delete(comment);
            }

            // Delete request
            _unitOfWork.CitizenRequirement
                .Delete(citizenRequest);

            await _unitOfWork.SaveChangesAsync();

            // Best-effort cleanup of the associated media, after the row is
            // already gone — an orphaned blob is recoverable; deleting media
            // for a request whose row-delete then fails is not.
            if (!string.IsNullOrWhiteSpace(citizenRequest.BlobName))
            {
                await _fileStorageService.DeleteFileAsync(
                    citizenRequest.BlobName,
                    FolderName);
            }

            return Result<string>.Success(
                "Citizen request deleted successfully.");
        }
    }
}