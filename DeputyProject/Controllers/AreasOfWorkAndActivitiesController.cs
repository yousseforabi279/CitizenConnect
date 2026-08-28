using Application.Core.Commands.Deputy.AreasOfWorkandActivities.CreateAreaOfWork;
using Application.Core.Commands.Deputy.AreasOfWorkandActivities.DeleteAreaOfWork;
using Application.Core.Commands.Deputy.AreasOfWorkandActivities.EditAreaofWork;
using Application.Core.Queries.Deputy.AreaOfWork.GetAll;
using Application.Core.Queries.Deputy.AreaOfWork.GetById;
using Bank.Api.Controllers;
using DeputyProject.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasOfWorkAndActivitiesController : BaseController
    {
        public AreasOfWorkAndActivitiesController(IMediator _mediator) : base(_mediator) { }
        [HttpGet(ApiRoutes.AreaOfWork.GETALL)]
        public async Task<IActionResult> GetAllAreasOfWork()
        {
            var result = await _mediator.Send(
                new GetAllAreasOfWorkQuery());

            return HandleResult(result);
        }
        [HttpGet(ApiRoutes.AreaOfWork.GETBYID)]
        public async Task<IActionResult> GetAreaOfWork(
               int areaId)
        {
            var result = await _mediator.Send(
                new GetAreaOfWorkQuery
                {
                    AreaId = areaId
                });

            return HandleResult(result);
        }
        [HttpPost(ApiRoutes.AreaOfWork.POST)]
        public async Task<IActionResult> CreateAreaOfWork(
                CreateAreaOfWorkCommand command)
        {

            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpPut(ApiRoutes.AreaOfWork.PUT)]
        public async Task<IActionResult> UpdateAreaOfWork(
                            int areaId,
                            UpdateAreaOfWorkCommand command)
        {
            command.AreaId = areaId;
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpDelete(ApiRoutes.AreaOfWork.DELETE)]
        public async Task<IActionResult> DeleteAreaOfWork(
                                int areaId)
        {
            var result = await _mediator.Send(
                new DeleteAreaOfWorkCommand
                {
                    AreaId = areaId
                });

            return HandleResult(result);
        }


    }
}
