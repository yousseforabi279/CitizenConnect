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
    public class CitizenRequirementContentRepo:GenericRepository<CitizenRequirementContent>, ICitizenRequirementContent
    {
        protected readonly ApplicationDbContext _context;
        public CitizenRequirementContentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
