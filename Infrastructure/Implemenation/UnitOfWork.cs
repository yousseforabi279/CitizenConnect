using Application.Contracts;
using Application.Contracts.Repos;
using Infrastructure.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Appcontext _context;

        public UnitOfWork(Appcontext context,
            ICitizinRequierment CitizinRequierment,
            IComplaintDepartment ComplaintDepartment,
            ICitizin citizin, IEmployee employee,
            IOrganization organization,
            IJwtTokenService jwtTokenService,
            IIdentityService identityService,
            IRefreshToken refreshToken,
            IRoleService roleService,
            IEmployeeRequestRepository employeeRequestRepository,
            ICitizinRequiermentEmployees citizinRequiermentEmployees )
        {
            _context = context;
            this.CitizinRequierment = CitizinRequierment;
            this.Department = ComplaintDepartment;
            Citizin = citizin;
            Employee = employee;
            Organization = organization;
            this.jwtTokenService = jwtTokenService;
            this.IdentityService = identityService;
            this.RefreshToken = refreshToken;
            this.RoleService = roleService;
            this.EmployeeRequestRepository = employeeRequestRepository;
            this.CitizinRequiermentEmployees = citizinRequiermentEmployees;

        }

        public ICitizinRequierment CitizinRequierment { get; }
        public IComplaintDepartment Department { get; }

        public ICitizin Citizin { get; }

        public IEmployee Employee { get; }

        public IOrganization Organization { get; }

        public IJwtTokenService jwtTokenService { get; }

        public IIdentityService IdentityService { get; }

        public IRefreshToken RefreshToken { get; }

        public IRoleService RoleService { get; }

        public ICitizinRequiermentEmployees CitizinRequiermentEmployees { get; }

        public IEmployeeRequestRepository EmployeeRequestRepository { get; }

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
            await _context.Database.CurrentTransaction!
                .CommitAsync(cancellationToken);
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