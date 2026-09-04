using Application.Common;
using Application.Core.Commands.LoadingPage.DeputyWords;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Deputy.DeputyWord.GetAll
{
    public record GetAllDeputyWordsQuery : IRequest<Result<List<DeputyWordsDTO>>>;
}
