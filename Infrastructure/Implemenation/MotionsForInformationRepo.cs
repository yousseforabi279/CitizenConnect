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
    public class MotionsForInformationRepo:GenericRepository<MotionsForInformation>, IMotionsForInformation
    {
        public MotionsForInformationRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
