using Application.Common;
using Application.Core.Queries.Deputy.DeputyWord.GetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.DeputyWord.GetById
{
    public record GetDeputyWordByIdQuery(int Id) : IRequest<Result<DeputyWordsDto>>;
}
