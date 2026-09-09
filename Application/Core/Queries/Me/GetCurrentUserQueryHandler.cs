using Application.Common;
using Application.Contracts;
using Application.Contracts.Repos;
using Application.Core.Queries.Me;
using MediatR;

namespace Application.Core.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler
        : IRequestHandler<GetCurrentUserQuery, Result<CurrentUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public GetCurrentUserQueryHandler(
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<CurrentUserResponse>> Handle(
            GetCurrentUserQuery request,
            CancellationToken cancellationToken)
        {
            // Get employee using the logged-in user's ID
            var employee =
                await _unitOfWork.Employee
                    .GetByUserIdAsync(_currentUser.UserId);

            var user = await _unitOfWork.IdentityService.FindByIdAsync(_currentUser.UserId);

            if (employee == null|| user == null)
            {
                return Result<CurrentUserResponse>.Failure(
                    ResultStatus.NotFound,
                    "Employee not found.");
            }

            var response = new CurrentUserResponse
            {
                UserName = employee.User.UserName!,
                Department = employee.Department.Name,

                Organizations = employee.EmployeeOrganizations
                    .Select(x => x.Organization.Name)
                    .ToList(),

                Roles = await _unitOfWork.IdentityService
                    .GetRolesAsync(user)
            };

            return Result<CurrentUserResponse>.Success(response);
        }
    }
}
