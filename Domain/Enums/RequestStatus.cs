using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum RequestStatus
    {
        New = 1,
        InProgress = 2,
        Resolved = 3,
        Rejected = 4,
        Closed = 5
    }
}
