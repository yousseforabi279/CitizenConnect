using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.AreaOfWork.GetAll
{
    public class GetAllAreasOfWorkQueryValidator
     : AbstractValidator<GetAllAreasOfWorkQuery>
    {
        public GetAllAreasOfWorkQueryValidator()
        {
        }
    }
}
