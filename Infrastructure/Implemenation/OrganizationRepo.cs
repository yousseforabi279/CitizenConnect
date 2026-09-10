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
    internal class OrganizationRepo : GenericRepository<Organization>, IOrganization
    {
        public OrganizationRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
