using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.ForgetPassword.GenerateNumericCode
{
    internal class CodeGeneratorService : ICodeGeneratorService
    {
        public string GenerateNumericCode(int length = 6)
        {
            var max = (int)Math.Pow(10, length);
            return RandomNumberGenerator.GetInt32(0, max).ToString(new string('0', length));
        }
        public string Hash(string code)
            => BCrypt.Net.BCrypt.HashPassword(code);

        public bool Verify(string code, string hash)
            => BCrypt.Net.BCrypt.Verify(code, hash);
    }
}
