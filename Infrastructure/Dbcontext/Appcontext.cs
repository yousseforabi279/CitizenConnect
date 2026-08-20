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
        public DbSet<Complaint> Complaintes { get; set; }
        public DbSet<ComplaintAssignment> ComplaintAssignments { get; set; }
        public DbSet<ComplaintCategory> complaintCategories { get; set; }
        public DbSet<ComplaintComment> ComplaintComments { get; set; }
        public DbSet<ComplaintResolution> complaintResolutions { get; set; }
        public DbSet<ComplaintStatusHistory> ComplaintStatusHistories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }


    }
}
