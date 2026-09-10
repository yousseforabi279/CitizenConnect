using Application.Core.Commands.Deputy.ActivityVisit.CreateActivityVisit;
using Application.Core.Commands.Deputy.ActivityVisit.DeleteActivityVisit;
using Application.Core.Commands.Deputy.ActivityVisit.EditActivityVisit;
using Application.Core.Queries.Deputy.ActivityVisit.GetAll;
using Application.Core.Queries.Deputy.ActivityVisit.GetAllById;
using DeputyProject.Controllers;
using DeputyProject.Common;
using DeputyProject.Mappers;
using DeputyProject.Requests.ActivityVisit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityVisitController : BaseController
    {
        public ActivityVisitController(IMediator _mediator) : base(_mediator) { }
        [HttpGet(ApiRoutes.ActivitiesVisits.GETALL)]
        public async Task<IActionResult> GetAllActivityVisits()
        {
            var result = await _mediator.Send(
                new GetAllActivityVisitsQuery());

            return HandleResult(result);
        }

        [HttpGet(ApiRoutes.ActivitiesVisits.GETBYID)]
        public async Task<IActionResult> GetActivityVisit(
                         int ActivityVisitId)
        {
            var result = await _mediator.Send(
                new GetActivityVisitQuery
                {
                    ActivityVisitId = ActivityVisitId
                });

            return HandleResult(result);
        }
    
        [Authorize(Roles = "Employee")]
        [HttpPost(ApiRoutes.ActivitiesVisits.POST)]
        public async Task<IActionResult> AddActivityVisit([FromForm] CreateActivitiesVisiteRequest request)
        {
            var command = new CreateActivityVisitCommand
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                Date = request.Date,
                Media = request.Media.MapToFileUploadRequest()
            };
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [Authorize(Roles = "Employee")]
        [HttpPut(ApiRoutes.ActivitiesVisits.PUT)]
        public async Task<IActionResult> UpdateActivityVisit(
                                                    int ActivityVisitId, [FromForm] UpdateActivitiesVisiteRequest request)
        {
            var command = new UpdateActivityVisitCommand
            {
                Id = ActivityVisitId,
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                Date = request.Date,
                Media = request.Media.MapToFileUploadRequest()
            };
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }


     
        [Authorize(Roles = "Employee")]
        [HttpDelete(ApiRoutes.ActivitiesVisits.DELETE)]
        public async Task<IActionResult> DeleteActivityVisit(int ActivityVisitId)
        {
            var result = await _mediator.Send(
                new DeleteActivityVisitCommand
                {
                    ActivityVisitId = ActivityVisitId
                });

            return HandleResult(result);
        }
    }
}
