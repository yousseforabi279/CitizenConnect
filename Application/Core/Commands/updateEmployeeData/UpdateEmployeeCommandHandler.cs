using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Commands.updateEmployee;
using Application.storage;
using Domain;
using Domain.Deputy;
using MediatR;
using System;
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
            var userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                return Result<int>.Failure(ResultStatus.Unauthorized, "غير مصرح لك بالوصول.");
            }

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                // resolve the current user's own employee record — never trust a client-supplied id here
                var employeeLookup = await _unitOfWork.Employee.GetByUserIdAsync(userId);
                if (employeeLookup is null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<int>.Failure(ResultStatus.NotFound, "بيانات الموظف غير موجودة.");
                }

                var employee = await _unitOfWork.Employee.GetEmpwithitsdata(employeeLookup.Id);
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
                    employee.Image.MediaType = upload.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase)
                        ? MediaType.Image
                        : MediaType.Other;
                    employee.Image.UploadedAt = DateTime.UtcNow;
                    employee.Image.MediaUrl = upload.BlobName != null
                        ? _fileStorageService.GetFileUrl(upload.BlobName, "employees")
                        : null;

                    if (!string.IsNullOrWhiteSpace(oldBlobName))
                    {
                        await _fileStorageService.DeleteFileAsync(oldBlobName, "employees");
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return Result<int>.Success(employee.Id, "تم تحديث بيانات الملف الشخصي بنجاح.");
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<int>.Failure(ResultStatus.Failure, "فشل تحديث الملف الشخصي.");
            }
        }
    }
}