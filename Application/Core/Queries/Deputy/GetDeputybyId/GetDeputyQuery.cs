using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.GetDeputybyId
{
    public class GetDeputyQuery : IRequest<Result<DeputyResponse>>
    {
        public int Id { get; set; }

        public GetDeputyQuery(int id)
        {
            Id = id;
        }
    }
}
