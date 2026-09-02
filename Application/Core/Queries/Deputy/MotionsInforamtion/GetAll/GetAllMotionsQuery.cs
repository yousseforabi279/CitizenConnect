using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.MotionsInforamtion.GetAll
{

    namespace Application.Core.Queries.Deputy.MotionsForInformation.GetAll
    {
        public record GetAllMotionsQuery
            : IRequest<Result<List<MotionDto>>>;
    }
}
