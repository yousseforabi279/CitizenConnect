using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.achievements.DeleteAchievement
{
    public class DeleteAchievementCommandValidator
      : AbstractValidator<DeleteAchievementCommand>
    {
        public DeleteAchievementCommandValidator()
        {

            RuleFor(x => x.AchievementId)
                .GreaterThan(0)
                .WithMessage("الإنجاز غير صحيح.");
        }
    }
}
