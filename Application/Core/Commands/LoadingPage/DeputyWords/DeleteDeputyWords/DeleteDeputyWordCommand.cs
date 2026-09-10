using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.DeleteDeputyWords
{
    public class DeleteDeputyWordCommand : IRequest<Result<int>>
    {
        public int DeputyWordId { get; set; }
        public DeleteDeputyWordCommand(int DeputyWordId)
        {
            this.DeputyWordId= DeputyWordId;
        }
    }
}
