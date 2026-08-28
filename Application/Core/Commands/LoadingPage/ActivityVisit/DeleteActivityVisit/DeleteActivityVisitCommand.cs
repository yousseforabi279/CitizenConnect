using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.ActivityVisit.DeleteActivityVisit
{
    public class DeleteActivityVisitCommand : IRequest<Result<int>>
    {
        public int ActivityVisitId { get; set; }
    }
}
