using Domain.Deputy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.LoadingPage.PersonalInfo.EditPersonalInfo
{
    public class PersonalInfoDTO
    {

        public string MediaUrl { get; set; }
        public string ContentType { get; set; }
        public MediaType? MediaType { get; set; }
    }
}
