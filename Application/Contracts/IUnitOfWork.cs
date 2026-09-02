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
        ICitizinRequierment CitizinRequierment { get; }
        IComplaintDepartment Department { get; }
        ICitizin Citizin { get; }
        IEmployee Employee { get; }
        IOrganization Organization { get; }
        IJwtTokenService jwtTokenService { get; }
        IIdentityService IdentityService { get; }
        IRefreshToken RefreshToken { get; }
        IRoleService RoleService { get; }
        ICitizinRequiermentEmployees CitizinRequiermentEmployees { get; }
        IEmployeeRequestRepository EmployeeRequestRepository { get; }
        IDeputy Deputy { get; }
        IAchievement Achievement { get; }
        IActitvitiesAndVisits ActitvitiesAndVisits { get; }
        IAreasOfWorkandActivities AreasOfWorkandActivities { get; }
        IMotionsForInformation MotionsForInformation { get; }
        IDeputyword Deputyword { get; }
        IPasswordResetCode PasswordResetCode { get; }
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
