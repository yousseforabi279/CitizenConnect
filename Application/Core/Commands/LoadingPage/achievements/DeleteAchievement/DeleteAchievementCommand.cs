using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.achievements.DeleteAchievement
{
    public class DeleteAchievementCommand : IRequest<Result<int>>
    {
        public int AchievementId { get; set; }
    }
}
