using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Commands.CreateCompliant.Validation;
using Application.storage;
using AutoMapper;
using Domain;
using Domain.Deputy;
using Domain.Enums;
using MediatR;

namespace Application.Core.Commands.CreateCompliant
{
    public class CreateCompliantCommandHandler
        : IRequestHandler<CreateCompliantCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INationalIdValidator _nationalId;
        private readonly IFileStorageService _fileStorageService;

        private const string FolderName = "request-files";

        public CreateCompliantCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            INationalIdValidator nationalId,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _nationalId = nationalId;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<string>> Handle(
            CreateCompliantCommand request,
            CancellationToken cancellationToken)
        {
            if (!_nationalId.IsValid(request.NationalId))
            {
                return Result<string>.Failure(
                    ResultStatus.ValidationError,
                    "Invalid national ID.");
            }

            var department =
                await _unitOfWork.Department
                    .GetByIdAsync(request.DepartmentId);

            if (department is null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Complaint or Suggestion category not found.");
            }

            var organization =
                await _unitOfWork.Organization
                    .GetByIdAsync(request.OrganizationId);

            if (organization is null)
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "Complaint or Suggestion direction not found.");
            }

            var citizen =
                await _unitOfWork.Citizin
                    .GetByNationalidAsync(request.NationalId);

            if (citizen is null)
            {
                citizen = new Citizen
                {
                    NationalId = request.NationalId,
                    FullName = request.FullName,
                    BirthDate = request.BirthDate,
                    Phone = request.Phone
                };

                await _unitOfWork.Citizin.AddAsync(citizen);
            }

            var requirement = new CitizinRequierment
            {
                Type = request.RequestType,
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                Citizen = citizen,
                Status = RequestStatus.New
            };

            var employees =
                await _unitOfWork.Employee
                    .GetAvailableEmployeesAsync(
                        request.DepartmentId,
                        request.OrganizationId);

            if (employees is null || !employees.Any())
            {
                return Result<string>.Failure(
                    ResultStatus.NotFound,
                    "No employees found for this department and organization.");
            }

            foreach (var employee in employees)
            {
                requirement.Employees.Add(
                    new CitizinRequiermentEmployee
                    {
                        Employee = employee,
                        CitizinRequierment = requirement
                    });
            }

            // Upload media to Cloudinary
            if (request.Media != null)
            {
                var upload =
                    await _fileStorageService.UploadFileAsync(
                        request.Media,
                        FolderName);

                requirement.BlobName = upload.BlobName;
                requirement.MediaFileName = request.Media.FileName;
                requirement.ContentType = upload.ContentType;
                requirement.FileSizeBytes = upload.SizeBytes;

                requirement.MediaType =
                    request.Media.ContentType.StartsWith("video/")
                        ? MediaType.Video
                        : MediaType.Image;

                requirement.UploadedAt = DateTime.UtcNow;

                requirement.MediaUrl =
                    _fileStorageService.GetFileUrl(
                        requirement.BlobName,
                        FolderName);
            }

            await _unitOfWork.CitizinRequierment
                .AddAsync(requirement);

            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(
                "Request created and assigned successfully.");
        }
    }
}