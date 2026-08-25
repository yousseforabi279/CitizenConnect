using Application.Common;
using Application.Contracts.Repos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    internal class RoleServiceRepo : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleServiceRepo(
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<(bool,string)> CreateRoleAsync(string roleName)
        {
            var exists = await _roleManager
            .RoleExistsAsync(roleName);

            if (exists)
            {
                return (false,"The Role Already Exist");
            }
            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            
            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));

                return (false,errors);
            }
            return (true,"Role created successfully.");
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            var exists = await _roleManager.RoleExistsAsync(roleName);
            return exists;
        }
    }

}
