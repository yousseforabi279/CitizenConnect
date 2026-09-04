using Domain.Deputy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.AreasOfWorkandActivities
{
    public class AreaOfWorkDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? MediaUrl { get; set; }
        public string? ContentType { get; set; }
        public MediaType? MediaType { get; set; }
    }
}
