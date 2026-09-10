using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface ICitizenRequirementEmployees : IGenericRepository<CitizenRequirementEmployee>
    {
        Task<CitizenRequirementEmployee?> GetAssignmentAsync(int requirementId,
    int employeeId);
    }
}
