using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.achievements.CreateAchievement
{
    public class CreateAchievementCommand : IRequest<Result<int>>
    {

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Image { get; set; } = null!;

    }
}
