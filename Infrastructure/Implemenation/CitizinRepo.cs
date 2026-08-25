using Application.Contracts.Repos;
using Domain;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class CitizinRepo : GenericRepository<Citizen>, ICitizin
    {
        protected readonly Appcontext _context;

        public CitizinRepo(Appcontext context) : base(context)
        {
            _context = context;
        }

        public async Task<Citizen?> GetByNationalidAsync(string id)
        {
            return await _context.Citizens.SingleOrDefaultAsync(ww=>ww.NationalId == id);
        }
    }
}
