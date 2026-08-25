using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.CreateCompliant.Validation
{
    public interface INationalIdValidator
    {
        bool IsValid(string nationalId);
    }
}
