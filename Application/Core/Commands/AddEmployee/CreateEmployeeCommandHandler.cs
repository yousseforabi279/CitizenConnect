using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.AddEmployee
{
    internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await _unitOfWork.IdentityService.CreateUserAsync(
                                request.Email,
                                request.Password,
                                request.FullName);
                if (!result.Success)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<int>.Failure(
                        ResultStatus.BadRequest,
                        result.Error!);
                }
                var user = result.User!;

                // Every account created through this endpoint is staff (self-service
                // registration is not exposed to citizens). The role is fixed here
                // rather than taken from the request to prevent a caller from
                // requesting an arbitrary, auto-created privileged role.
                const string EmployeeRole = "Employee";

                if (!await _unitOfWork.RoleService.RoleExistsAsync(EmployeeRole))
                {
                    var res = await _unitOfWork.RoleService.CreateRoleAsync(EmployeeRole);
                    if (!res.Item1)
                    {
                        await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                        return Result<int>.Failure(
                        ResultStatus.Failure,
                        res.Item2);
                    }
                }

                var roleAdded = await _unitOfWork.IdentityService.AddToRoleAsync(user, EmployeeRole);
                if (!roleAdded)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<int>.Failure(
                        ResultStatus.BadRequest,
                    "Could not assign Employee role.");
                }

                var employee = new Employee
                {
                    UserId = user.Id,
                    DepartmentId = request.DepartmentId,
                    IsActive = true,
                    
                };
                await _unitOfWork.Employee.AddAsync(employee);
                employee.EmployeeOrganizations.Add(new EmployeeOrganizations { EmployeeId = employee.Id, OrganizationId = request.organiztionId });
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return Result<int>.Success(
                employee.Id,
                "Employee created successfully.");
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(
                 cancellationToken);

                return Result<int>.Failure(
                    ResultStatus.Failure,
                    "Failed to create employee.");
            }
        }
    }
}