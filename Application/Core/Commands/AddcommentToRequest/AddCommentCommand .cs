using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.AddcommentToRequest
{
    public class AddCommentCommand : IRequest<Result<string>>
    {
        public int CitizinRequiermentId { get; set; }
        public string Comment { get; set; } = null!;
    }
}
