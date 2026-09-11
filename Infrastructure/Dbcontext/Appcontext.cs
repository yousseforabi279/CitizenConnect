using Domain;
using Domain.Deputy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dbcontext
{
    public class Appcontext:IdentityDbContext<User>
    {
        public Appcontext(DbContextOptions<Appcontext> options)
        : base(options)
        {
        }   
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<CitizinRequierment> CitizinRequierments { get; set; }
        public DbSet<CitizinRequiermentContent> CitizinRequiermentContents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<CitizinRequiermentEmployee> CitizinRequiermentEmployees { get; set; }

        public DbSet<Deputy> Deputies { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public DbSet<DeputyWords> DeputyWords { get; set; }

        public DbSet<Achievement> Achievements { get; set; }

        public DbSet<ActitvitiesAndVisits> ActitvitiesAndVisits { get; set; }

        public DbSet<AreasOfWorkandActivities> AreasOfWorkAndActivities { get; set; }

        public DbSet<MotionsForInformation> MotionsForInformation { get; set; }
        public DbSet<PasswordResetCode> passwordResetCodes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CitizinRequierment>()
                .Property(x => x.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.UserId)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasQueryFilter(e => e.IsActive);

            // EmployeeImage — already fixed
            modelBuilder.Entity<EmployeeImage>()
                .HasOne(ei => ei.Employee)
                .WithOne(e => e.Image)
                .HasForeignKey<EmployeeImage>(ei => ei.EmployeeId)
                .IsRequired(false);

            // EmployeeOrganizations
            modelBuilder.Entity<EmployeeOrganizations>()
                .HasOne(eo => eo.Employee)
                .WithMany(e => e.EmployeeOrganizations)
                .HasForeignKey(eo => eo.EmployeeId)
                .IsRequired(false);

            // CitizinRequiermentEmployee
            modelBuilder.Entity<CitizinRequiermentEmployee>()
                .HasOne(cre => cre.Employee)
                .WithMany(e => e.Requests)
                .HasForeignKey(cre => cre.EmployeeId)
                .IsRequired(false);

            // CitizinRequiermentContent
            modelBuilder.Entity<CitizinRequiermentContent>()
                .HasOne(crc => crc.Employee)
                .WithMany()
                .HasForeignKey(crc => crc.EmployeeId)
                .IsRequired(false);
        }

    }
}
