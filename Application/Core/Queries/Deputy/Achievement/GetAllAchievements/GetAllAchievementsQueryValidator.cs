using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.Achievement.GetAllAchievements
{
    public class GetAllAchievementsQueryValidator
      : AbstractValidator<GetAllAchievementsQuery>
    {
        public GetAllAchievementsQueryValidator()
        {
            RuleFor(x => x.DeputyId)
                .GreaterThan(0)
                .WithMessage("النائب غير صحيح.");
        }
    }
}
