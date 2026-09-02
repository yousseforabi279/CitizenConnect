using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Employee.GetEmployeeInfo
{
    public record GetEmployeeInfoQuery() : IRequest<Result<EmployeeInfoResponse>>;

}
