using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface IRoleService
    {
        Task<(bool,string)> CreateRoleAsync(string roleName);
        Task<bool> RoleExistsAsync(string roleName);
    }
}
