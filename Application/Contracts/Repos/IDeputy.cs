using Application.Common;
using Application.Core.Queries.GetRequestsForDeputy;
using Domain;
using Domain.Deputy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface IDeputy : IGenericRepository<Deputy>
    {
        Task<Deputy?> GetDeputyInfo();
        Task<PaginatedResult<DeputyRequestDto>> GetAllForDeputyAsync(
    DeputyRequestFilter filter,
    CancellationToken cancellationToken);
    }
}
