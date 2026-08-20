using Application.Common;
using MediatR;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.CreateCompliant
{
    public class CreateCompliantCommand:IRequest<Result<int>>
    {
        public string? CitizenName { get; set; }
        public string? NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public ComplaintPriority Priority { get; set; }

        // images and videos 
    }
}
