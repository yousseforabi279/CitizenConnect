using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Queries.Employee.GetEmployeeInfo;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Queries.GetEmployeeInfo
{
    public class GetEmployeeInfoQueryHandler : IRequestHandler<GetEmployeeInfoQuery, Result<EmployeeInfoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

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

            var employee = await _unitOfWork.Employee.GetByUserIdAsync(userId);
            if (employee is null)
            {
                return Result<EmployeeInfoResponse>.Failure(
                    ResultStatus.NotFound,
                    "بيانات الموظف غير موجودة.");
            }

            var res=await _unitOfWork.Employee.GetEmplyeeInfo(userId);
            var response = new EmployeeInfoResponse
            {
                Name = res.Name,
                Department = res.Department
            };

            return Result<EmployeeInfoResponse>.Success(response, "تم جلب بيانات الموظف بنجاح.");
        }
    }
}