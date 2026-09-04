using Application.Common;
using Application.Core.Commands.LoadingPage.achievements;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.achievements.EditAchievement
{
    public class UpdateAchievementCommand : IRequest<Result<AchievementDto>>
    {
        public int AchievementId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public FileUploadRequest Media { get; set; } // null = keep existing media

    }
}
