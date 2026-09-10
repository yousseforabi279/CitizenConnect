using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;

namespace Application.Core.Commands.DeleteCitizenRequest
{
    public class DeleteCitizenRequestCommandHandler
        : IRequestHandler<DeleteCitizenRequestCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "request-files";

        public DeleteCitizenRequestCommandHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
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

            // Delete media from Cloudinary
            if (!string.IsNullOrWhiteSpace(citizenRequest.BlobName))
            {
                await _fileStorageService.DeleteFileAsync(
                    citizenRequest.BlobName,
                    FolderName);
            }

            // Delete employees relations
            foreach (var employee in citizenRequest.Employees)
            {
                _unitOfWork.CitizenRequirementEmployees
                    .Delete(employee);
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

            return Result<string>.Success(
                "Citizen request deleted successfully.");
        }
    }
}