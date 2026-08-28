using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.DeleteAreaOfWork
{
    public class DeleteAreaOfWorkCommand : IRequest<Result<int>>
    {
        public int AreaId { get; set; }
    }
}
