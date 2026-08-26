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
        [HttpPost(ApiRoutes.ActivitiesVisits.POST)]
        public async Task<IActionResult> AddAchievement(CreateActivityVisitCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpPut(ApiRoutes.ActivitiesVisits.PUT)]
        public async Task<IActionResult> UpdateActivityVisit(
                                                    int deputyId,
                                                    int activityId,
                                                    UpdateActivityVisitCommand command)
        {
            command.DeputyId = deputyId;
            command.ActivityVisitId = activityId;

            var result = await _mediator.Send(command);

            return HandleResult(result);
        }

        [HttpDelete(ApiRoutes.ActivitiesVisits.DELETE)]
        public async Task<IActionResult> DeleteActivityVisit(
                                                    int deputyId,
                                                    int activityId)
        {
            var result = await _mediator.Send(
                new DeleteActivityVisitCommand
                {
                    DeputyId = deputyId,
                    ActivityVisitId = activityId
                });

            return HandleResult(result);
        }
        [HttpGet(ApiRoutes.ActivitiesVisits.GETBYID)]
        public async Task<IActionResult> GetActivityVisit(
                            int deputyId,
                            int activityId)
        {
            var result = await _mediator.Send(
                new GetActivityVisitQuery
                {
                    DeputyId = deputyId,
                    ActivityVisitId = activityId
                });

            return HandleResult(result);
        }
        [HttpGet(ApiRoutes.ActivitiesVisits.GETALL)]
        public async Task<IActionResult> GetAllActivityVisits(int deputyId)
        {
            var result = await _mediator.Send(
                new GetAllActivityVisitsQuery
                {
                    DeputyId = deputyId
                });

            return HandleResult(result);
        }
    }
}
