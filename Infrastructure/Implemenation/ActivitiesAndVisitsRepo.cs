using Application.Contracts.Repos;
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
    internal class ActivitiesAndVisitsRepo:GenericRepository<ActivitiesAndVisits>, IActivitiesAndVisits
    {
        public ActivitiesAndVisitsRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
