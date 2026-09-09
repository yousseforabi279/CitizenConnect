using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.DeleteCitizenRequest
{
    public class DeleteCitizenRequestCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }

        public DeleteCitizenRequestCommand(int id)
        {
            Id = id;
        }
    }
}
