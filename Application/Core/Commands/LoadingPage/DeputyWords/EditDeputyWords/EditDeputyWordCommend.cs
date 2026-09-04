using Application.Common;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.DeputyWords.EditDeputyWords
{
    public class UpdateDeputyWordsCommand : IRequest<Result<DeputyWordsDTO>>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public FileUploadRequest? Media { get; set; } // null = keep existing media
    }
}
