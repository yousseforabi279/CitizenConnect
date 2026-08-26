using Application.Contracts.Repos;
using Domain;
using Domain.Deputy;
using Infrastructure.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class DeputyRepo : GenericRepository<Deputy>, IDeputy
    {
        protected readonly Appcontext _context;

        public DeputyRepo(Appcontext context) : base(context)
        {
            _context = context;
        }

    }
}
