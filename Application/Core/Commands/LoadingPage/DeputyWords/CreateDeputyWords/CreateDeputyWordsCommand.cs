using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords
{   
    public class CreateDeputyWordsCommand : IRequest<Result<int>>
    {

        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;

    }
}
