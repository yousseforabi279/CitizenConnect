using Domain;
using Infrastructure.Dbcontext;
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
            services.AddIdentityCore<User>()
                    .AddEntityFrameworkStores<Appcontext>();
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
            return services;
        }

    }
}
