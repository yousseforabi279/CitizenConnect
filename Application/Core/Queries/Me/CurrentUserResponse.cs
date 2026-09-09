using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Queries.Me
{
    public class CurrentUserResponse { 
        public string UserName { get; set; } = null!;
        public string Department { get; set; } = null!;
        public List<string> Organizations { get; set; } = new();
        public List<string> Roles { get; set; }
    }
}
