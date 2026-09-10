using Application.Core.Commands.CreateComplaint;
using Application.Core.Commands.Deputy.achievements.CreateAchievement;
using Application.Core.Commands.LoadingPage.PersonalInfo.EditPersonalInfo;
using Application.Core.Queries.Deputy.GetDeputybyId;
using Azure.Core;
using DeputyProject.Controllers;
using DeputyProject.Common;
using DeputyProject.Mappers;
using DeputyProject.Requests.DeputyInfo;
using DeputyProject.SeedDataDto;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeputyController : BaseController
    {
        protected readonly ApplicationDbContext _context;
        public DeputyController(IMediator _mediator, ApplicationDbContext context) : base(_mediator) { _context = context; }
 

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
        [HttpGet(ApiRoutes.Deputy.GetDeputy)]
        public async Task<IActionResult> CreateComplaint()
        {
            var result = await _mediator.Send(new GetDeputyQuery());
            return HandleResult(result);
        }
      
    }
}
       
   