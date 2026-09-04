using Application.Common;
using Application.Core.Commands.LoadingPage.achievements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.Achievement.GetAchievementById
{
    public class GetAchievementQuery
    : IRequest<Result<AchievementDto>>
    {
        public int AchievementId { get; set; }
    }
}
