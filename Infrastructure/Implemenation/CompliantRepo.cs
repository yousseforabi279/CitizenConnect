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
    public class CompliantRepo : GenericRepository<CitizinRequierment>, ICitizinRequierment
    {
        public CompliantRepo(Appcontext context) : base(context) { }
     
            public async Task<CitizinRequierment?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.CitizinRequierments
                .Include(x => x.Citizen)
                .Include(x => x.Employees)
                    .ThenInclude(x => x.Employee)
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}


