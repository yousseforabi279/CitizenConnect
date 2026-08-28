using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.AreaOfWork.GetById
{
    public class GetAreaOfWorkQueryValidator
     : AbstractValidator<GetAreaOfWorkQuery>
    {
        public GetAreaOfWorkQueryValidator()
        {

            RuleFor(x => x.AreaId)
                .GreaterThan(0)
                .WithMessage("مجال العمل غير صحيح.");
        }
    }
}
