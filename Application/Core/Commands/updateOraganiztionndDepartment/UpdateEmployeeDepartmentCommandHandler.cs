using Application.Common;
using Application.Contracts;
using Application.Core.Commands.updateOraganiztionndDepartment;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.UpdateEmployeeDepartment
{
    internal class UpdateEmployeeDepartmentCommandHandler
        : IRequestHandler<UpdateEmployeeDepartmentCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateEmployeeDepartmentCommand request, CancellationToken cancellationToken)
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

                if (request.DepartmentId > 0)
                {
                    employee.DepartmentId = request.DepartmentId;
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

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return Result<int>.Success(employee.Id, "Employee department/organizations updated successfully.");
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<int>.Failure(ResultStatus.Failure, "Failed to update employee department.");
            }
        }
    }
}