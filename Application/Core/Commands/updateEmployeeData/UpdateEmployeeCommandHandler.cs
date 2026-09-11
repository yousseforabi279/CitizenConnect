using Application.Common;
using Application.Contracts;
using Application.storage;
using Domain;
using Domain.Deputy;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.updateEmployee
{
    internal class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService fileStorageService;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            this.fileStorageService = fileStorageService;
        }

        public async Task<Result<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var employee = await _unitOfWork.Employee.GetEmpwithitsdata(request.EmployeeId);
                if (employee is null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<int>.Failure(ResultStatus.NotFound, "Employee not found.");
                }

                if (request.About != null)
                {
                    employee.about = request.About;
                }

                if (request.DepartmentId > 0)
                {
                    employee.DepartmentId = request.DepartmentId;
                }

                if (request.OrganizationIds is not null && request.OrganizationIds.Count > 0)
                {
                    employee.EmployeeOrganizations.Clear();

                    foreach (var orgId in request.OrganizationIds.Distinct())
                    {
                        employee.EmployeeOrganizations.Add(new EmployeeOrganizations
                        {
                            EmployeeId = employee.Id,
                            OrganizationId = orgId
                        });
                    }
                }

                if (request.Image is not null)
                {
                    var oldBlobName = employee.Image?.BlobName;

                    var upload = await fileStorageService.UploadFileAsync(request.Image, "employees");

                    if (employee.Image is null)
                    {
                        employee.Image = new EmployeeImage { EmployeeId = employee.Id };
                    }

                    employee.Image.BlobName = upload.BlobName;
                    employee.Image.MediaFileName = request.Image.FileName;
                    employee.Image.ContentType = upload.ContentType;
                    employee.Image.FileSizeBytes = upload.SizeBytes;
                    employee.Image.MediaType = upload.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase)
                        ? MediaType.Image
                        : MediaType.Other;
                    employee.Image.UploadedAt = DateTime.UtcNow;
                    employee.Image.MediaUrl = upload.BlobName != null
                        ? fileStorageService.GetFileUrl(upload.BlobName, "employees")
                        : null;

                    if (!string.IsNullOrWhiteSpace(oldBlobName))
                    {
                        // delete after successful upload so we never end up with zero images
                        await fileStorageService.DeleteFileAsync(oldBlobName, "employees");
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return Result<int>.Success(employee.Id, "Employee updated successfully.");
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<int>.Failure(ResultStatus.Failure, "Failed to update employee.");
            }
        }
    }
}