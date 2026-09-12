using Application.Common;
using Application.Contracts;
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

        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Employee.GetByUserIdAsync(request.EmployeeId);

            if (employee is null)
                return Result<int>.Failure(ResultStatus.NotFound, "Employee not found.");

            employee.IsActive = false;

            await _unitOfWork.SaveChangesAsync();

            return Result<int>.Success(employee.Id, "Employee deactivated successfully.");
        }
    }
}
