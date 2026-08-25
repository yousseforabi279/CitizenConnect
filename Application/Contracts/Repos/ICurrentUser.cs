using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface ICurrentUser
    {
        string? UserId { get; }

        bool IsAuthenticated { get; }

        bool IsInRole(string role);
    }
}
