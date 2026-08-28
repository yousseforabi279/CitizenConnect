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
        [HttpGet(ApiRoutes.Achievements.GetAllAchievements)]
        public async Task<IActionResult> GetAllAchievements()
        {
            var result = await _mediator.Send(
                new GetAllAchievementsQuery());

            return HandleResult(result);
        }

        [HttpGet(ApiRoutes.Achievements.GetAchievementById)]
        public async Task<IActionResult> GetAchievement(int achievementId)
        {
            var result = await _mediator.Send(
                new GetAchievementQuery
                {
                    AchievementId = achievementId
                });
            return HandleResult(result);
        }
        [HttpPost(ApiRoutes.Achievements.CreateAchievement)]
        public async Task<IActionResult> AddAchievement(CreateAchievementCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpPut(ApiRoutes.Achievements.EditAchivement)]
        public async Task<IActionResult> UpdateAchievement(int AchievementId,
                                                         UpdateAchievementCommand command)
        {
            command.AchievementId = AchievementId;
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
      
        [HttpDelete(ApiRoutes.Achievements.DeleteAchievements)]
        public async Task<IActionResult> DeleteAchievement(int AchievementId)
        {
            var result = await _mediator.Send(
                new DeleteAchievementCommand
                {
                    AchievementId = AchievementId
                });

            return HandleResult(result);
        }

    }
}
