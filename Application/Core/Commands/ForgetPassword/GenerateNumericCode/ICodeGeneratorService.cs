using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.ForgetPassword.GenerateNumericCode
{
  public interface ICodeGeneratorService
    {
        string GenerateNumericCode(int length = 6);
        string Hash(string code);
        bool Verify(string code, string hash);

    }
}
