using Application.Core.Commands.CreateCompliant;
using Application.Core.Commands.Deputy.achievements.CreateAchievement;
using Application.Core.Commands.LoadingPage.PersonalInfo.EditPersonalInfo;
using Application.Core.Queries.Deputy.GetDeputybyId;
using Azure.Core;
using Bank.Api.Controllers;
using DeputyProject.Common;
using DeputyProject.Mappers;
using DeputyProject.Requests.DeputyInfo;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeputyController : BaseController
    {
        public DeputyController(IMediator _mediator) : base(_mediator) { }

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
                BirthOfdate= updatePersonalInfo.BirthOfdate,
                Circle= updatePersonalInfo.Circle,
                FacebookLing= updatePersonalInfo.FacebookLing,
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
        [HttpGet(ApiRoutes.Deputy.GetDeputy)]
        public async Task<IActionResult> CreateComplaint()
        {
            var result = await _mediator.Send(new GetDeputyQuery());
            return HandleResult(result);
        }
       
    }
}
