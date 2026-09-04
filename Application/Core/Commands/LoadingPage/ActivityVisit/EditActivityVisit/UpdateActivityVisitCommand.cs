using Application.Common;
using Application.Core.Commands.LoadingPage.ActivityVisit;
using Application.storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.ActivityVisit.EditActivityVisit
{
    public class UpdateActivityVisitCommand : IRequest<Result<ActivityVisitDTO>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public FileUploadRequest Media { get; set; } // null = keep existing
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}
