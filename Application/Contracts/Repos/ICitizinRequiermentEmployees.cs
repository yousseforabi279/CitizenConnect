using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface ICitizinRequiermentEmployees : IGenericRepository<CitizinRequiermentEmployee>
    {
        Task<CitizinRequiermentEmployee?> GetAssignmentAsync(int requirementId,
    int employeeId);
    }
}
