using Application.Core.Commands.Deputy.achievements.CreateAchievement;
using Application.Core.Commands.Deputy.achievements.DeleteAchievement;
using Application.Core.Commands.Deputy.achievements.EditAchievement;
using Application.Core.Queries.Deputy.Achievement.GetAchievementById;
using Application.Core.Queries.Deputy.Achievement.GetAllAchievements;
using Bank.Api.Controllers;
using DeputyProject.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : BaseController
    {
        public AchievementController(IMediator _mediator) : base(_mediator) { }
        [HttpPost(ApiRoutes.Achievements.CreateAchievement)]
        public async Task<IActionResult> AddAchievement(CreateAchievementCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpPut(ApiRoutes.Achievements.EditAchivement)]
        public async Task<IActionResult> UpdateAchievement(int achievementId,
                                                         UpdateAchievementCommand command)
        {
            command.AchievementId = achievementId;
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpGet(ApiRoutes.Achievements.GetAchievementById)]
        public async Task<IActionResult> GetAchievement(int deputyId,int achievementId)
        {
            var result = await _mediator.Send(
                new GetAchievementQuery
                {
                    DeputyId = deputyId,
                    AchievementId = achievementId
                });
            return HandleResult(result);
        }
        [HttpGet(ApiRoutes.Achievements.GetAllAchievements)]
        public async Task<IActionResult> GetAllAchievements(int deputyId)
        {
            var result = await _mediator.Send(
                new GetAllAchievementsQuery
                {
                    DeputyId = deputyId
                });

            return HandleResult(result);
        }
        [HttpDelete(ApiRoutes.Achievements.DeleteAchievements)]
        public async Task<IActionResult> DeleteAchievement(int deputyId,int achievementId)
        {
            var result = await _mediator.Send(
                new DeleteAchievementCommand
                {
                    DeputyId = deputyId,
                    AchievementId = achievementId
                });

            return HandleResult(result);
        }

    }
}
