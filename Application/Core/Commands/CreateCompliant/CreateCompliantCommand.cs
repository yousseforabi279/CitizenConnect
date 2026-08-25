using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Core.Commands.CreateCompliant
{
    public class CreateCompliantCommand:IRequest<Result<string>>
    {
        public RequestType  RequestType { get; set; }

        // Citizen information
        public string NationalId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public string Phone { get; set; } = null!;

        // Complaint information
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int DepartmentId { get; set; }
        public int OrganizationId { get; set; }


        // images and videos 
    }
}
