using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.EditDeputyWords
{
    public class EditDeputyWordCommend : IRequest<Result<int>>
    {
        public int DeputyWordId { get; set; }
        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;

    }
}
