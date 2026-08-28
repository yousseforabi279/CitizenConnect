using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.ActivityVisit.GetAllById
{
    public class GetActivityVisitQueryValidator
      : AbstractValidator<GetActivityVisitQuery>
    {
        public GetActivityVisitQueryValidator()
        {

            RuleFor(x => x.ActivityVisitId)
                .GreaterThan(0)
                .WithMessage("النشاط أو الزيارة غير صحيح.");
        }
    }
}
