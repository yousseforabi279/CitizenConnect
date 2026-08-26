using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.Achievement.GetAchievementById
{
    public class GetAchievementQuery
    : IRequest<Result<AchievementResponse>>
    {
        public int DeputyId { get; set; }

        public int AchievementId { get; set; }
    }
}
