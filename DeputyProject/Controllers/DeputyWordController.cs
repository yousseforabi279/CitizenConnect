using Application.Core.Commands.LoadingPage.DeputyWords.CreateDeputyWords;
using Application.Core.Commands.LoadingPage.DeputyWords.DeleteDeputyWords;
using Application.Core.Commands.LoadingPage.DeputyWords.EditDeputyWords;
using Application.Core.Queries.Deputy.DeputyWord.GetAll;
using Application.Core.Queries.Deputy.DeputyWord.GetById;
using DeputyProject.Controllers;
using DeputyProject.Common;
using DeputyProject.Mappers;
using DeputyProject.Requests.DeputyWord;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Employee")]
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
        [Authorize(Roles = "Employee")]
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
        [Authorize(Roles = "Employee")]
        [HttpDelete(ApiRoutes.DeputyWord.DELETE)]
        public async Task<IActionResult> DeleteDeputyWord(int DeputyWordId)
        {
           
            var result = await _mediator.Send(new DeleteDeputyWordCommand(DeputyWordId));
            return HandleResult(result);
        }
    }
}
