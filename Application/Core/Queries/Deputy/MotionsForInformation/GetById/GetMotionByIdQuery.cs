using Application.Common;
using Application.Core.Commands.LoadingPage.MotionsForInformation;
using Application.Core.Queries.Deputy.MotionsForInformation.GetAll;
using MediatR;

namespace Application.Core.Queries.Deputy.MotionsForInformation.GetById
{
    public record GetMotionByIdQuery(int MotionId)
        : IRequest<Result<MotionsForInformationDTO>>;
}