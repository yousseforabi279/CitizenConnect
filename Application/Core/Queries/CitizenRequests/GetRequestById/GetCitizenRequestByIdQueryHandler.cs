using Application.Common;
using Application.Contracts;
using Application.storage;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.CitizenRequests.GetRequestById
{
    public class GetCitizenRequestByIdQueryHandler
         : IRequestHandler<GetCitizenRequestByIdQuery, Result<CitizenRequestDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private const string ContainerName = "Request-files";

        public GetCitizenRequestByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            this._blobStorageService = blobStorageService;
        }

        public async Task<Result<CitizenRequestDto>> Handle(GetCitizenRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var citizenRequest =
                await _unitOfWork.CitizinRequierment
                    .GetByIdWithDetailsAsync(request.Id);

            if (citizenRequest is null)
            {
                return Result<CitizenRequestDto>.Failure(
                    ResultStatus.NotFound,
                    "Citizen request not found.");
            }
            var result = new CitizenRequestDto
            {
                Id = citizenRequest.Id,

                // Citizen
                CitizenNationalId = citizenRequest.CitizenNationalId,
                CitizenFullName = citizenRequest.Citizen.FullName,
                BirthDate = citizenRequest.Citizen.BirthDate,
                Phone = citizenRequest.Citizen.Phone,

                // Request
                Type = citizenRequest.Type,
                Title = citizenRequest.Title,
                Description = citizenRequest.Description,
                Status = citizenRequest.Status,
                Priority = citizenRequest.Priority,
                CreatedAt = citizenRequest.CreatedAt,

                // Media
                MediaFileName = citizenRequest.MediaFileName,
                ContentType = citizenRequest.ContentType,
                FileSizeBytes = citizenRequest.FileSizeBytes,
                MediaType = citizenRequest.MediaType,
                UploadedAt = citizenRequest.UploadedAt
            };
            if (!string.IsNullOrEmpty(citizenRequest.BlobName))
            {
                result.MediaUrl =
                    _blobStorageService.GetReadSasUrl(
                        citizenRequest.BlobName,
                        ContainerName);
            }

            return Result<CitizenRequestDto>.Success(result);
        }
    }
}
