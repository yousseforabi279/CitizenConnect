using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Commands.updateEmployee;
using Application.storage;
using Domain;
using Domain.Deputy;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.UpdateEmployeeProfile
{
    internal class UpdateEmployeeProfileCommandHandler
        : IRequestHandler<UpdateEmployeeProfileCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly IFileStorageService _fileStorageService;
        private const string FolderName = "employees";

        public UpdateEmployeeProfileCommandHandler(
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<int>> Handle(UpdateEmployeeProfileCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {


                var employee = await _unitOfWork.Employee.GetEmpwithitsdata(request.EmployeeId);
                if (employee is null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<int>.Failure(ResultStatus.NotFound, "بيانات الموظف غير موجودة.");
                }
               
                if (request.About != null)
                {
                    employee.about = request.About;
                }

                if (!string.IsNullOrWhiteSpace(request.Phone))
                {
                    employee.User.PhoneNumber = request.Phone;
                }

                if (!string.IsNullOrWhiteSpace(request.FullName))
                {
                    employee.User.FullName = request.FullName;
                }

                if (request.DepartmentId is > 0)
                {
                    employee.DepartmentId = request.DepartmentId.Value;
                }

                if (request.OrganizationIds is not null && request.OrganizationIds.Count > 0)
                {
                    employee.EmployeeOrganizations.Clear();

                    foreach (var orgId in request.OrganizationIds.Distinct())
                    {
                        employee.EmployeeOrganizations.Add(new Domain.EmployeeOrganizations
                        {
                            EmployeeId = employee.Id,
                            OrganizationId = orgId
                        });
                    }
                }

                if (request.Image is not null)
                {
                    var oldBlobName = employee.Image?.BlobName;

                    var upload = await _fileStorageService.UploadFileAsync(request.Image, "employees");

                    if (employee.Image is null)
                    {
                        employee.Image = new EmployeeImage { EmployeeId = employee.Id };
                    }

                    employee.Image.BlobName = upload.BlobName;
                    employee.Image.MediaFileName = request.Image.FileName;
                    employee.Image.ContentType = upload.ContentType;
                    employee.Image.FileSizeBytes = upload.SizeBytes;
                    employee.Image.MediaType = ResolveMediaType(upload.ContentType);
                    employee.Image.UploadedAt = DateTime.UtcNow;
                    employee.Image.MediaUrl = upload.BlobName != null
                      ? _fileStorageService.GetFileUrl(upload.BlobName, FolderName, upload.ContentType)
                      : null;

                    if (!string.IsNullOrWhiteSpace(oldBlobName))
                    {
                        await _fileStorageService.DeleteFileAsync(oldBlobName, FolderName);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return Result<int>.Success(employee.Id, "تم تحديث بيانات الموظف بنجاح.");
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<int>.Failure(ResultStatus.Failure, "فشل تحديث بيانات الموظف.");
            }
        }
        private static MediaType ResolveMediaType(string contentType)
        {
            if (contentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                return MediaType.Image;

            if (contentType.StartsWith("video/", StringComparison.OrdinalIgnoreCase))
                return MediaType.Video;

            return MediaType.Other;
        }
    }
}
