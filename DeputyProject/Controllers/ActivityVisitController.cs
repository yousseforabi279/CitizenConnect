using Application.Core.Commands.Deputy.achievements.CreateAchievement;
using Application.Core.Commands.Deputy.ActivityVisit.CreateActivityVisit;
using Application.Core.Commands.Deputy.ActivityVisit.DeleteActivityVisit;
using Application.Core.Commands.Deputy.ActivityVisit.EditActivityVisit;
using Application.Core.Queries.Deputy.ActivityVisit.GetAll;
using Application.Core.Queries.Deputy.ActivityVisit.GetAllById;
using Bank.Api.Controllers;
using DeputyProject.Common;
using MediatR;
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
                         int activityId)
        {
            var result = await _mediator.Send(
                new GetActivityVisitQuery
                {
                    ActivityVisitId = activityId
                });

            return HandleResult(result);
        }
    
        [HttpPost(ApiRoutes.ActivitiesVisits.POST)]
        public async Task<IActionResult> AddAchievement(CreateActivityVisitCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpPut(ApiRoutes.ActivitiesVisits.PUT)]
        public async Task<IActionResult> UpdateActivityVisit(
                                                    int ActivityVisitId,
                                                    UpdateActivityVisitCommand command)
        {
            command.ActivityVisitId = ActivityVisitId;

            var result = await _mediator.Send(command);

            return HandleResult(result);
        }


     
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
