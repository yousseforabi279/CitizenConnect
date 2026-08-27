using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.Deputy.ActivityVisit.EditActivityVisit
{
    public class UpdateActivityVisitCommand : IRequest<Result<int>>
    {
        public int DeputyId { get; set; }

        public int ActivityVisitId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Image_Video { get; set; }

        public string Location { get; set; } = null!;

        public DateTime Date { get; set; }
    }
}
