using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface ICitizenRequirement : IGenericRepository <CitizenRequirement>
    {
        Task<CitizenRequirement?> GetByIdWithDetailsAsync(int id);
    }
}
