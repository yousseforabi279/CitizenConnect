using Application.Contracts.Repos;
using Domain;
using Infrastructure.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class OrganizationRepo : GenericRepository<Organization>, IOrganization
    {
        protected readonly Appcontext _context;

        public OrganizationRepo(Appcontext context) : base(context)
        {
            _context = context;
        }

    }
}
