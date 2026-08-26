using Domain.Deputy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface IActitvitiesAndVisits:IGenericRepository<ActitvitiesAndVisits>
    {
        Task<List<ActitvitiesAndVisits>> GetByDeputyIdAsync(
        int deputyId,
        CancellationToken cancellationToken);
    }
}
