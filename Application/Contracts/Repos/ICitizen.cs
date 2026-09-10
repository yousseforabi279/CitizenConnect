using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface ICitizen : IGenericRepository<Citizen>
    {
        Task<Citizen?> GetByNationalidAsync(string id);

    }
}
