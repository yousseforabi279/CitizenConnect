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
    public class MotionsForInformationRepo:GenericRepository<MotionsForInformation>, IMotionsForInformation
    {
        protected readonly Appcontext _context;

        public MotionsForInformationRepo(Appcontext context) : base(context)
        {
            _context = context;
        }
    }
}
