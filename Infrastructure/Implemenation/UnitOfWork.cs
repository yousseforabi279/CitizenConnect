using Application.Contracts;
using Application.Contracts.Repos;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            ICitizenRequirement citizenRequirement,
            IDepartment department,
            ICitizen citizen, IEmployee employee,
            IOrganization organization,
            IJwtTokenService jwtTokenService,
            IIdentityService identityService,
            IRefreshToken refreshToken,
            IRoleService roleService,
            IEmployeeRequestRepository employeeRequestRepository,
            ICitizenRequirementEmployees citizenRequirementEmployees,
            IDeputy deputy,
            IAchievement achievement,
            IActivitiesAndVisits activitiesAndVisits,
            IAreasOfWorkAndActivities areasOfWorkAndActivities,
            IDeputyWord deputyWord,
            IMotionsForInformation motionsForInformation,
            IPasswordResetCode passwordResetCode,
            IEmailService emailService,
            ICitizenRequirementContent citizenRequirementContent
            )
        {
            _context = context;
            CitizenRequirement = citizenRequirement;
            Department = department;
            Citizen = citizen;
            Employee = employee;
            Organization = organization;
            this.jwtTokenService = jwtTokenService;
            IdentityService = identityService;
            RefreshToken = refreshToken;
            RoleService = roleService;
            EmployeeRequestRepository = employeeRequestRepository;
            CitizenRequirementEmployees = citizenRequirementEmployees;
            Deputy = deputy;
            Achievement = achievement;
            ActivitiesAndVisits = activitiesAndVisits;
            AreasOfWorkAndActivities = areasOfWorkAndActivities;
            DeputyWord = deputyWord;
            MotionsForInformation = motionsForInformation;
            PasswordResetCode = passwordResetCode;
            EmailService = emailService;
            CitizenRequirementContent = citizenRequirementContent;
        }

        public ICitizenRequirement CitizenRequirement { get; }
        public IDepartment Department { get; }

        public ICitizen Citizen { get; }

        public IEmployee Employee { get; }

        public IOrganization Organization { get; }

        public IJwtTokenService jwtTokenService { get; }

        public IIdentityService IdentityService { get; }

        public IRefreshToken RefreshToken { get; }

        public IRoleService RoleService { get; }

        public ICitizenRequirementEmployees CitizenRequirementEmployees { get; }

        public IEmployeeRequestRepository EmployeeRequestRepository { get; }

        public IDeputy Deputy {  get; }

        public IAchievement Achievement { get; }

        public IActivitiesAndVisits ActivitiesAndVisits { get; }

        public IAreasOfWorkAndActivities AreasOfWorkAndActivities { get; }

        public IDeputyWord DeputyWord {  get; }

        public IMotionsForInformation MotionsForInformation {get; }

        public IPasswordResetCode PasswordResetCode { get; }

        public IEmailService EmailService { get; }

        public ICitizenRequirementContent CitizenRequirementContent {  get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync(
        CancellationToken cancellationToken = default)
        {
            await _context.Database.BeginTransactionAsync(
                cancellationToken);
        }

        public async Task CommitTransactionAsync(
            CancellationToken cancellationToken = default)
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.CurrentTransaction
                    .CommitAsync(cancellationToken);
            }
        }

        public async Task RollbackTransactionAsync(
            CancellationToken cancellationToken = default)
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.CurrentTransaction
                    .RollbackAsync(cancellationToken);
            }
        }

    }
}