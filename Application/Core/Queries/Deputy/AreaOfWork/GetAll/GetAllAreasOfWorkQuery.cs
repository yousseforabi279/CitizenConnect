using Application.Common;
using Application.Core.Commands.LoadingPage.AreasOfWorkandActivities;
using Application.Core.Queries.Deputy.AreaOfWork.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.AreaOfWork.GetAll
{
    public class GetAllAreasOfWorkQuery
       : IRequest<Result<List<AreaOfWorkDTO>>>
    {
    }
}
