using Application.Common;
using Application.Core.Commands.LoadingPage.ActivityVisit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.ActivityVisit.GetAll
{
    public class GetAllActivityVisitsQuery
     : IRequest<Result<List<ActivityVisitDTO>>>
    {
    }
}
