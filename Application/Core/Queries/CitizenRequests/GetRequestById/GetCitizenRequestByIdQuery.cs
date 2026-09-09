using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.CitizenRequests.GetRequestById
{
    public class GetCitizenRequestByIdQuery
           : IRequest<Result<CitizenRequestDto>>
    {
        public int Id { get; set; }

        public GetCitizenRequestByIdQuery(int id)
        {
            Id = id;
        }
    }
}
