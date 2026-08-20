using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public enum ResultStatus
    {
        Success,
        ValidationError,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        RequiresTwoFactor,
        Failure,
        BadRequest,
        InternalServerError
    }
}
