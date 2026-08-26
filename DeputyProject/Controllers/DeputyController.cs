using Application.Core.Commands.CreateCompliant;
using Application.Core.Commands.Deputy.achievements.CreateAchievement;
using Application.Core.Commands.Deputy.EditDeputyInfo;
using Application.Core.Queries.Deputy.GetDeputybyId;
using Bank.Api.Controllers;
using DeputyProject.Common;
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
        public async Task<IActionResult> UpdateDeputy(UpdateDeputyCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpGet(ApiRoutes.Deputy.GetDeputy)]
        public async Task<IActionResult> CreateComplaint(int id)
        {
            var result = await _mediator.Send(new GetDeputyQuery(id));
            return HandleResult(result);
        }
       
    }
}
