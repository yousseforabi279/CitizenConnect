using Application.Contracts;
using Application.Contracts.Repos;
using Domain;
using Infrastructure.Dbcontext;
using Infrastructure.Implemenation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration) 
        {
            // Register DbContext
            services.AddDbContext<Appcontext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Connection")
                ));

            // Register ASP.NET Core Identity
            services
               .AddIdentityCore<Domain.User>(options =>
               {
                   // Password settings
                   options.Password.RequiredLength = 8;
                   options.Password.RequireDigit = true;
                   options.Password.RequireUppercase = true;
                   options.Password.RequireLowercase = true;
                   options.Password.RequireNonAlphanumeric = false;

                   // User settings
                   options.User.RequireUniqueEmail = true;

                   // Lockout settings
                   options.Lockout.MaxFailedAccessAttempts = 5;
                   options.Lockout.DefaultLockoutTimeSpan =
                       TimeSpan.FromMinutes(5);
               })
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<Appcontext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICitizinRequierment, CompliantRepo>();
            services.AddScoped<IComplaintDepartment, ComplaintCategoryRepo>();
            services.AddScoped<ICitizin, CitizinRepo>();
            services.AddScoped<IEmployee, EmployeeRepo>();
            services.AddScoped<IOrganization, OrganizationRepo>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IRefreshToken, RefreshToekenRepo>();
            services.AddScoped<IRoleService, RoleServiceRepo>();
            services.AddScoped<ICitizinRequiermentEmployees, CitizinRequiermentEmployees>();
            services.AddScoped<IEmployeeRequestRepository, EmployeeRequestRepository>();

            return services;
        }

    }
}
