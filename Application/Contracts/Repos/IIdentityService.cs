using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface IIdentityService
    {
        Task<User?> FindByEmailAsync(string email);

        Task<bool> CheckPasswordAsync(
            User user,
            string password);

        Task<IList<string>> GetRolesAsync(
            User user);

        Task<(bool Success, User? User, string? Error)>CreateUserAsync(
            string email,
            string password, string Fullname);

        Task<bool> AddToRoleAsync(
            User user,
            string role);
    }
}
