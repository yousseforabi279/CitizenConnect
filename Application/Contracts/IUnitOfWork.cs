using Application.Contracts.Repos;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IUnitOfWork
    {
        ICitizenRequirement CitizenRequirement { get; }
        IDepartment Department { get; }
        ICitizen Citizen { get; }
        IEmployee Employee { get; }
        IOrganization Organization { get; }
        IJwtTokenService jwtTokenService { get; }
        IIdentityService IdentityService { get; }
        IRefreshToken RefreshToken { get; }
        IRoleService RoleService { get; }
        ICitizenRequirementEmployees CitizenRequirementEmployees { get; }
        IEmployeeRequestRepository EmployeeRequestRepository { get; }
        IDeputy Deputy { get; }
        IAchievement Achievement { get; }
        IActivitiesAndVisits ActivitiesAndVisits { get; }
        IAreasOfWorkAndActivities AreasOfWorkAndActivities { get; }
        IMotionsForInformation MotionsForInformation { get; }
        IDeputyWord DeputyWord { get; }
        IPasswordResetCode PasswordResetCode { get; }
        ICitizenRequirementContent CitizenRequirementContent { get; }
        IEmailService EmailService { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync(
            CancellationToken cancellationToken = default);

        Task CommitTransactionAsync(
            CancellationToken cancellationToken = default);

        Task RollbackTransactionAsync(
            CancellationToken cancellationToken = default);
    }
}
