using Application.Common;
using Application.Contracts;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.DeleteEmplyee
{
    internal class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork,IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Employee.GetByUserIdAsync(request.EmployeeId);

            if (employee is null)
                return Result<int>.Failure(ResultStatus.NotFound, "الموظف غير موجود .");
            var imageBlobName = employee.Image?.BlobName;
            var imageContentType = employee.Image?.ContentType;
            employee.IsActive = false;

            await _unitOfWork.SaveChangesAsync();


            if (!string.IsNullOrWhiteSpace(imageBlobName))
            {
                // Do this after the DB commit succeeds so a failed Cloudinary
                // delete doesn't block employee deletion. Consider wrapping
                // in try/catch + logging if you don't want it to fail the whole request.
                await _fileStorageService.DeleteFileAsync(imageBlobName, imageContentType);
            }
            return Result<int>.Success(employee.Id, "تم حذف الموظف بنجاح .");
        }
    }
}
