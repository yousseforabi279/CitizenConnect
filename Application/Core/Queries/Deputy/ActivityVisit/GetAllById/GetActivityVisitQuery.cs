using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.ActivityVisit.GetAllById
{
    public class GetActivityVisitQuery
     : IRequest<Result<ActivityVisitResponse>>
    {
        public int ActivityVisitId { get; set; }
    }
}
