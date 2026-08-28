using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.Achievement.GetAchievementById
{
    public class GetAchievementQueryValidator
     : AbstractValidator<GetAchievementQuery>
    {
        public GetAchievementQueryValidator()
        {

            RuleFor(x => x.AchievementId)
                .GreaterThan(0)
                .WithMessage("الإنجاز غير صحيح.");
        }
    }
}
