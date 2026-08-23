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
        public DbSet<Citizen> Complaintes { get; set; }
        public DbSet<CitizinRequierment> ComplaintAssignments { get; set; }
        public DbSet<CitizinRequiermentContent> complaintCategories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }


    }
}
