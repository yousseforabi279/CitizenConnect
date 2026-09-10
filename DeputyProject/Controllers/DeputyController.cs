using Application.Core.Commands.LoadingPage.PersonalInfo.EditPersonalInfo;
using Application.Core.Queries.Deputy.GetDeputybyId;
using DeputyProject.Controllers;
using DeputyProject.Common;
using DeputyProject.Mappers;
using DeputyProject.Requests.DeputyInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeputyController : BaseController
    {
        public DeputyController(IMediator _mediator) : base(_mediator) { }

        [Authorize(Roles = "Employee")]
        [HttpPut(ApiRoutes.Deputy.Edit)]
        public async Task<IActionResult> UpdateDeputy([FromForm] UpdatePersonalInfo updatePersonalInfo)
        {
            var command = new UpdateDeputyCommand
            {
                AboutPart1 = updatePersonalInfo.AboutPart1,
                AboutPart2= updatePersonalInfo.AboutPart2,
                Address = updatePersonalInfo.Address,
                Appointment= updatePersonalInfo.Appointment,
                Bio= updatePersonalInfo.Bio,
                BirthDate= updatePersonalInfo.BirthDate,
                Circle= updatePersonalInfo.Circle,
                FacebookLink= updatePersonalInfo.FacebookLink,
                FullName= updatePersonalInfo.FullName,
                LocationURL= updatePersonalInfo.LocationURL,
                OfficeLocation=updatePersonalInfo.OfficeLocation,
                PrimaryPhone= updatePersonalInfo.PrimaryPhone,
                SecondaryPhone= updatePersonalInfo.SecondaryPhone,
                Title= updatePersonalInfo.Title,
                WhatsApp= updatePersonalInfo.WhatsApp,
                Media = updatePersonalInfo.Media.MapToFileUploadRequest()

            };
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        // Public-facing "about the deputy" page — no auth required to read.
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Deputy.GetDeputy)]
        public async Task<IActionResult> GetDeputy()
        {
            var result = await _mediator.Send(new GetDeputyQuery());
            return HandleResult(result);
        }
      
    }
}
       
   