using Domain;
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
        }

    }
}
