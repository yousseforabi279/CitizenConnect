using Application.Contracts.Repos;
using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;

        public IdentityService(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _userManager
                .FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(
            User user,
            string password)
        {
            return await _userManager
                .CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetRolesAsync(
            User user)
        {
            return await _userManager
                .GetRolesAsync(user);
        }

        public async Task<(bool Success, User? User, string? Error)> CreateUserAsync(string email, string password,string Fullname)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                FullName = Fullname
               
            };

            var result = await _userManager.CreateAsync(
                user,
                password);
            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));
                return (
                    false,
                    null,
                    errors
                );
            }
            return (
                true,
                user,
                null
        );
        }

        public async Task<bool> AddToRoleAsync(User user, string role)
        {
            var result = await _userManager
                       .AddToRoleAsync(user, role);
            return result.Succeeded;
        }
    }
}
