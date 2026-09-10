using Application.Contracts.Repos;
using Domain;
using Domain.Deputy;
using Infrastructure.Data;
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
        protected readonly ApplicationDbContext _context;
        public AchievementRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
