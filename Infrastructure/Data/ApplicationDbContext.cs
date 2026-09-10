using Domain;
using Domain.Deputy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext:IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<CitizenRequirement> CitizenRequirements { get; set; }
        public DbSet<CitizenRequirementContent> CitizenRequirementContents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<CitizenRequirementEmployee> CitizenRequirementEmployees { get; set; }

        public DbSet<Deputy> Deputies { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public DbSet<DeputyWords> DeputyWords { get; set; }

        public DbSet<Achievement> Achievements { get; set; }

        public DbSet<ActivitiesAndVisits> ActivitiesAndVisits { get; set; }

        public DbSet<AreasOfWorkAndActivities> AreasOfWorkAndActivities { get; set; }

        public DbSet<MotionsForInformation> MotionsForInformation { get; set; }
        public DbSet<PasswordResetCode> PasswordResetCodes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CitizenRequirement>(entity =>
            {
                entity.Property(x => x.Type)
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.HasIndex(x => x.Status);
                entity.HasIndex(x => x.CreatedAt);

                // A department can be deleted without losing the citizen
                // requirements that were assigned to it — they just become
                // unassigned rather than being deleted or blocking the delete.
                entity.HasOne(x => x.Department)
                    .WithMany(d => d.CitizenRequirements)
                    .HasForeignKey(x => x.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .IsUnique();

                // Employees carry an IsActive flag for soft-deletion; a
                // department with employees still assigned should not be
                // deletable (and must never silently cascade-delete staff).
                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CitizenRequirementEmployee>(entity =>
            {
                entity.HasIndex(x => new { x.CitizenRequirementId, x.EmployeeId })
                    .IsUnique();

                // Protects the citizen-facing assignment history: removing an
                // employee must not silently delete which requests they were
                // assigned to.
                entity.HasOne(x => x.Employee)
                    .WithMany(e => e.Requests)
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CitizenRequirementContent>(entity =>
            {
                // Protects the citizen-facing comment history the same way.
                entity.HasOne(x => x.Employee)
                    .WithMany()
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmployeeOrganizations>(entity =>
            {
                entity.HasIndex(x => new { x.EmployeeId, x.OrganizationId })
                    .IsUnique();
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasOne(r => r.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(r => r.TokenHash)
                    .IsUnique();
            });

            modelBuilder.Entity<PasswordResetCode>(entity =>
            {
                entity.HasOne(p => p.User)
                    .WithMany()
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(p => new { p.UserId, p.IsUsed });
            });
        }

    }
}
