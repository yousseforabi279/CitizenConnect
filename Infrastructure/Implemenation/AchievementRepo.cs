using Application.Contracts.Repos;
using Domain;
using Domain.Deputy;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    public class AchievementRepo : GenericRepository<Achievement>, IAchievement
    {
        protected readonly Appcontext _context;
        public AchievementRepo(Appcontext context) : base(context)
        {
            _context = context;
        }

    }
}
