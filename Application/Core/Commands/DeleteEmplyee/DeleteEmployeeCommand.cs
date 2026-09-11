using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.DeleteEmplyee
{
    public class DeleteEmployeeCommand : IRequest<Result<int>>
    {
        public int EmployeeId { get; set; }
    }

}
