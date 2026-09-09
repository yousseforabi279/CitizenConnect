using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.DeleteCitizenRequest
{
    public class DeleteCitizenRequestCommandHandler
        : IRequestHandler<DeleteCitizenRequestCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;

        private const string ContainerName = "Request-files";

        public DeleteCitizenRequestCommandHandler(
            IUnitOfWork unitOfWork,
            IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<string>> Handle(
            DeleteCitizenRequestCommand request,
            CancellationToken cancellationToken)
        {
            var citizenRequest =
                await _unitOfWork.CitizinRequierment
                    .GetByIdWithDetailsAsync(request.Id);

            if (citizenRequest is null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Citizen request not found.");
            }

            // Delete media from Blob Storage
            if (!string.IsNullOrEmpty(citizenRequest.BlobName))
            {
                await _blobStorageService.DeleteFileAsync(
                    citizenRequest.BlobName,
                    ContainerName);
            }

            // Delete request
            _unitOfWork.CitizinRequierment.Delete(citizenRequest);
            foreach (var employee in citizenRequest.Employees)
            {
                _unitOfWork.CitizinRequiermentEmployees.Delete(employee);
            }

            foreach (var comment in citizenRequest.Comments)
            {
                _unitOfWork.CitizinRequiermentContent.Delete(comment);
            }

            _unitOfWork.CitizinRequierment.Delete(citizenRequest);

            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(
                "Citizen request deleted successfully.");
        }
    }
}
