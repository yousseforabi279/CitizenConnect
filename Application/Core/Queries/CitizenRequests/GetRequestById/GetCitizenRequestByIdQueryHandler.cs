using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;

namespace Application.Core.Queries.CitizenRequests.GetRequestById
{
    public class GetCitizenRequestByIdQueryHandler
        : IRequestHandler<GetCitizenRequestByIdQuery, Result<CitizenRequestDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "request-files";

        public GetCitizenRequestByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<CitizenRequestDto>> Handle(
            GetCitizenRequestByIdQuery request,
            CancellationToken cancellationToken)
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

                Citizen = new CitizenInfoDto
                {
                    NationalId = citizenRequest.Citizen.NationalId,
                    FullName = citizenRequest.Citizen.FullName,
                    BirthDate = citizenRequest.Citizen.BirthDate,
                    Phone = citizenRequest.Citizen.Phone
                },

                Request = new RequestInfoDto
                {
                    Type = citizenRequest.Type,
                    Title = citizenRequest.Title,
                    Description = citizenRequest.Description,
                    Status = citizenRequest.Status,
                    Priority = citizenRequest.Priority,
                    CreatedAt = citizenRequest.CreatedAt
                },

                Media = new MediaInfoDto
                {
                    BlobName = citizenRequest.BlobName,
                    FileName = citizenRequest.MediaFileName,
                    ContentType = citizenRequest.ContentType,
                    FileSizeBytes = citizenRequest.FileSizeBytes,
                    MediaType = citizenRequest.MediaType,
                    UploadedAt = citizenRequest.UploadedAt,
                    MediaUrl = citizenRequest.MediaUrl
                },
                CommentDto = citizenRequest.Comments
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(x => new CommentDto
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        CreatedAt = x.CreatedAt,
                        EmployeeId = x.EmployeeId,
                        EmployeeName = x.Employee.User.FullName ?? ""
                    })
                    .ToList()
            };

            // Generate Cloudinary URL
            if (!string.IsNullOrEmpty(citizenRequest.BlobName))
            {
                result.Media.MediaUrl =
                    _fileStorageService.GetFileUrl(
                        citizenRequest.BlobName,
                        FolderName);
            }

            return Result<CitizenRequestDto>.Success(result);
        }
    }
}