using Application.Contracts.Repos;
using System.Security.Claims;

namespace DeputyProject.Common
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId =>
            _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?
                .User
                .Identity?
                .IsAuthenticated ?? false;

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext?
                .User
                .IsInRole(role) ?? false;
        }
    }
}
