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

                if (!await _unitOfWork.RoleService.RoleExistsAsync(request.Role))
                {
                    var res = await _unitOfWork.RoleService.CreateRoleAsync(request.Role);
                    if (!res.Item1)
                    {
                        await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                        return Result<int>.Failure(
                        ResultStatus.Failure,
                        res.Item2);
                    }
                }

                var roleAdded = await _unitOfWork.IdentityService.AddToRoleAsync(user, request.Role);
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
                    IsActive = true
                };

                await _unitOfWork.Employee.AddAsync(employee);
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