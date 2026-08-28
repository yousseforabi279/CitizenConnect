using Application.Common;
using Application.Core.Queries.Deputy.Achievement.GetAchievementById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.Achievement.GetAllAchievements
{
    public class GetAllAchievementsQuery
    : IRequest<Result<List<AchievementResponse>>>
    {
    }
}
