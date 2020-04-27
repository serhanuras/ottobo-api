using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Ottobo.Services
{
    public class HashService
    {
        public object Hash(string input)
        {
            // Generates a random salt 
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Hash(input, salt);
        }

        public object Hash(string input, byte[] salt)
        {
            // derives a 256 bits subkey (uses HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
          password: input,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA1,
          iterationCount: 10000,
          numBytesRequested: 256 / 8));

            return new 
            {
                Hash = hashed,
                Salt = salt
            };
        }
    }
}