using Application.Contracts.Repos;
using Domain.Deputy;
using Infrastructure.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class DeputywordRepo:GenericRepository<DeputyWords>,IDeputyword
    {
        protected readonly Appcontext _context;

        public DeputywordRepo(Appcontext context) : base(context)
        {
            _context = context;
        }
    }
}
