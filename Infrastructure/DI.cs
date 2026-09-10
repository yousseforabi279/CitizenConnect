using Application.Contracts;
using Application.Contracts.Repos;
using Application.storage;
using Domain;
using Infrastructure.Data;
using Infrastructure.Implemenation;
using Infrastructure.Services;
using Infrastructure.Settings;
using Infrastructure.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration) 
        {
            // Register DbContext
            var connectionString =
                   Environment.GetEnvironmentVariable("MSSQL_TCP_URL")
                   ?? configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Connection String Exists: {!string.IsNullOrEmpty(connectionString)}");

            services.AddDbContext<Infrastructure.Data.ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Register ASP.NET Core Identity
            //services
            //   .AddIdentityCore<Domain.User>(options =>
            //   {
            //       // Password settings
            //       options.Password.RequiredLength = 8;
            //       options.Password.RequireDigit = true;
            //       options.Password.RequireUppercase = true;
            //       options.Password.RequireLowercase = true;
            //       options.Password.RequireNonAlphanumeric = false;

            //       // User settings
            //       options.User.RequireUniqueEmail = true;

            //       // Lockout settings
            //       options.Lockout.MaxFailedAccessAttempts = 5;
            //       options.Lockout.DefaultLockoutTimeSpan =
            //           TimeSpan.FromMinutes(5);
            //   })
            //   .AddRoles<IdentityRole>()
            //   .AddEntityFrameworkStores<ApplicationDbContext>()
            //   .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICitizenRequirement, CitizenRequirementRepo>();
            services.AddScoped<IDepartment, DepartmentRepo>();
            services.AddScoped<ICitizen, CitizenRepo>();
            services.AddScoped<IEmployee, EmployeeRepo>();
            services.AddScoped<IOrganization, OrganizationRepo>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IRefreshToken, RefreshTokenRepo>();
            services.AddScoped<IRoleService, RoleServiceRepo>();
            services.AddScoped<ICitizenRequirementEmployees, CitizenRequirementEmployees>();
            services.AddScoped<IEmployeeRequestRepository, EmployeeRequestRepository>();
            services.AddScoped<IDeputy, DeputyRepo>();
            services.AddScoped<IAchievement, AchievementRepo>();
            services.AddScoped<IActivitiesAndVisits, ActivitiesAndVisitsRepo>();
            services.AddScoped<IAreasOfWorkAndActivities, AreasOfWorkAndActivitiesRepo>();
            services.AddScoped<IDeputyWord, DeputyWordRepo>();
            services.AddScoped<IMotionsForInformation, MotionsForInformationRepo>();
            services.AddScoped<IPasswordResetCode, PasswordResetCodeRepo>();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileStorageService, CloudinaryStorageService>();
            services.AddScoped<ICitizenRequirementContent, CitizenRequirementContentRepo>();



            return services;
        }

    }
}
