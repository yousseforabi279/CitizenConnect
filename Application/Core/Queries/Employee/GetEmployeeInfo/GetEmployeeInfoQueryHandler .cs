using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Queries.Employee.GetEmployeeInfo;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetEmployeeInfo
{
    public class GetEmployeeInfoQueryHandler : IRequestHandler<GetEmployeeInfoQuery, Result<EmployeeInfoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        private static readonly string[] PrivilegedRoles = { "Admin", "Social" };

        public GetEmployeeInfoQueryHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<EmployeeInfoResponse>> Handle(GetEmployeeInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                return Result<EmployeeInfoResponse>.Failure(
                    ResultStatus.Unauthorized,
                    "غير مصرح لك بالوصول.");
            }

            var user = await _unitOfWork.IdentityService.FindByIdAsync(userId);
            if (user is null)
            {
                return Result<EmployeeInfoResponse>.Failure(
                    ResultStatus.NotFound,
                    "بيانات المستخدم غير موجودة.");
            }

            var roles = await _unitOfWork.IdentityService.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? string.Empty;

            // Admin / Social: no employee record required, return whatever basic info exists
            if (PrivilegedRoles.Contains(role))
            {
                var basicResponse = new EmployeeInfoResponse
                {
                    Name = user.FullName ?? string.Empty,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Department = null,
                    Organizations = new(),
                    Role = role
                };

                return Result<EmployeeInfoResponse>.Success(basicResponse, "تم جلب بيانات المستخدم بنجاح.");
            }

            // Employee (default) path: requires an employee record
            var employee = await _unitOfWork.Employee.GetByUserIdAsync(userId);
            if (employee is null)
            {
                return Result<EmployeeInfoResponse>.Failure(
                    ResultStatus.NotFound,
                    "بيانات الموظف غير موجودة.");
            }

            var res = await _unitOfWork.Employee.GetEmplyeeInfo(userId);
            var response = new EmployeeInfoResponse
            {
                Name = res.Name,
                Department = res.Department,
                ImageUrl = res.ImageUrl,
                Organizations = res.Organizations,
                Email = res.Email ?? user.Email,
                Phone = res.Phone ?? user.PhoneNumber,
                Role = role
            };

            return Result<EmployeeInfoResponse>.Success(response, "تم جلب بيانات الموظف بنجاح.");
        }
    }
}