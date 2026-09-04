using Application.Common;
using Application.Core.Commands.LoadingPage.AreasOfWorkandActivities;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.AreasOfWorkandActivities.EditAreaofWork
{
    public class UpdateAreaOfWorkCommand : IRequest<Result<AreaOfWorkDTO>>
    {
        public int AreaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public FileUploadRequest? Image { get; set; } // null = keep existing image
    }
}
