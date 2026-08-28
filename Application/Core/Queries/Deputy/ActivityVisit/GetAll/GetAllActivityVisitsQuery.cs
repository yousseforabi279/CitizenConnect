using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.ActivityVisit.GetAll
{
    public class GetAllActivityVisitsQuery
     : IRequest<Result<List<ActivityVisitResponse>>>
    {
    }
}
