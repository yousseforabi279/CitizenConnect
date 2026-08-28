using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.AreaOfWork.GetById
{
    public class GetAreaOfWorkQuery
     : IRequest<Result<AreaOfWorkResponse>>
    {

        public int AreaId { get; set; }
    }
}
