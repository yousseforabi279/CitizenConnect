using Application.Contracts.Repos;
using Domain;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class DepartmentRepo : GenericRepository<Department>, IDepartment
    {
        public DepartmentRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
