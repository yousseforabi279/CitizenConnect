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
    public class CitizinRequiermentContentRepo:GenericRepository<CitizinRequiermentContent>, ICitizinRequiermentContent
    {
        protected readonly Appcontext _context;
        public CitizinRequiermentContentRepo(Appcontext context) : base(context)
        {
            _context = context;
        }
    }
}
