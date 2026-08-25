using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface IEmployee : IGenericRepository<Employee>
    {
        public Task<List<Employee>> GetAvailableEmployeesAsync(int departmentId, int organizationId);
        public Task<Employee?> GetByUserIdAsync(string userId);
    }
}
