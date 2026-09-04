using Application.Core.Commands.Deputy.achievements.CreateAchievement;
using Application.Core.Commands.Deputy.achievements.EditAchievement;
using Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords;
using Application.Core.Commands.LoadingPage.DeputyWords.DeleteDeputyWords;
using Application.Core.Commands.LoadingPage.DeputyWords.EditDeputyWords;
using Application.Core.Queries.Deputy.Achievement.GetAchievementById;
using Application.Core.Queries.Deputy.Achievement.GetAllAchievements;
using Application.Core.Queries.Deputy.DeputyWord.GetAll;
using Application.Core.Queries.Deputy.DeputyWord.GetById;
using Bank.Api.Controllers;
using DeputyProject.Common;
using DeputyProject.Mappers;
using DeputyProject.Requests.Deputyword;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeputyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeputyWordController : BaseController
    {
        public DeputyWordController(IMediator _mediator) : base(_mediator) { }

        [HttpGet(ApiRoutes.DeputyWord.GETALL)]
        public async Task<IActionResult> GetAllADeputyWords()
        {
            var result = await _mediator.Send(
                new GetAllDeputyWordsQuery());

            return HandleResult(result);
        }

        [HttpGet(ApiRoutes.DeputyWord.GETBYID)]
        public async Task<IActionResult> GetDeputyWord(int DeputyWordId)
        {
            var result = await _mediator.Send(
                new GetDeputyWordByIdQuery(DeputyWordId));
            return HandleResult(result);
        }
        [HttpPost(ApiRoutes.DeputyWord.POST)]
        public async Task<IActionResult> AddDeputyWord([FromForm] CreateDeputyWordsRequest request)
        {
            var command = new CreateDeputyWordsCommand
            {
                Title = request.Title,
                Media = request.Media.MapToFileUploadRequest()
            };
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpPut(ApiRoutes.DeputyWord.PUT)]
        public async Task<IActionResult> UpdateDeputyWord(int DeputyWordId,
                                                        [FromForm] UpdateDeputyWordsRequest request)
        {
            var command = new UpdateDeputyWordsCommand
            {
                Id = DeputyWordId,
                Title = request.Title,
                Media = request.Media.MapToFileUploadRequest()
            };
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpDelete(ApiRoutes.DeputyWord.DELETE)]
        public async Task<IActionResult> DeleteDeputyWord(int DeputyWordId)
        {
           
            var result = await _mediator.Send(new DeleteDeputyWordCommend(DeputyWordId));
            return HandleResult(result);
        }
    }
}
