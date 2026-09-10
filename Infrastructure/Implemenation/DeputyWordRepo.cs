using Application.Contracts.Repos;
using Domain.Deputy;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class DeputyWordRepo:GenericRepository<DeputyWords>,IDeputyWord
    {
        protected readonly ApplicationDbContext _context;

        public DeputyWordRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
