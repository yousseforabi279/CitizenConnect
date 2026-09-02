using Application.Core.Commands.LoadingPage.MotionsForInformation.DeleteMotionsForInformation;
using Application.Core.Commands.LoadingPage.MotionsForInformation.EditMotionsForInformation;
using Application.Core.Queries.Deputy.MotionsForInformation.GetById;
using Application.Core.Queries.Deputy.MotionsInforamtion.GetAll.Application.Core.Queries.Deputy.MotionsForInformation.GetAll;
using Bank.Api.Controllers;
using DeputyProject.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotionController : BaseController
    {
        public MotionController(IMediator _mediator) : base(_mediator) { }

        [HttpGet(ApiRoutes.Motions.GETALL)]
        public async Task<IActionResult> GetAllMotions()
        {
            var result = await _mediator.Send(
                new GetAllMotionsQuery());

            return HandleResult(result);
        }
        [HttpGet(ApiRoutes.Motions.GETBYID)]
        public async Task<IActionResult> GetMotion(int MotionId)
        {
            var result = await _mediator.Send(
                new GetMotionByIdQuery(MotionId));

            return HandleResult(result);
        }

        [HttpPost(ApiRoutes.Motions.POST)]
        public async Task<IActionResult> AddMotion(
                CreateMotionCommand command)
        {
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }


        [HttpPut(ApiRoutes.Motions.PUT)]
        public async Task<IActionResult> UpdateMotion(
                            int MotionId,
                            EditMotionCommand command)
        {
            command.MotionId = MotionId;

            var result = await _mediator.Send(command);

            return HandleResult(result);
        }

        [HttpDelete(ApiRoutes.Motions.DELETE)]
        public async Task<IActionResult> DeleteMotion(int MotionId)
        {
            var result = await _mediator.Send(
                new DeleteMotionCommand(MotionId));

            return HandleResult(result);
        }
    }
}
