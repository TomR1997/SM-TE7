using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Grocerly.API.Utils
{
    public class PasswordHasher
    {
        private readonly RandomNumberGenerator _rng;

        public static string HashPassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes("Kaas");

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }

        public static string ValidatePassword(string password,string hash)
        {
            var hashed = PasswordHasher.HashPassword(password);

            return hashed.Equals(hash) ? hashed : null;
        }


    }
}
