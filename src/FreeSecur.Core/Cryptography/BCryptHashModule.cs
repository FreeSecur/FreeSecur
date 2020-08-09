using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptHasher = BCrypt.Net.BCrypt;

namespace FreeSecur.Core.Cryptography
{
    public class BCryptHashModule : IHashModule
    {
        public string GetHash(string plainText)
        {
            var hash = BCryptHasher.EnhancedHashPassword(plainText);

            return hash;
        }

        public bool Verify(string plainText, string hash)
        {
            return BCryptHasher.EnhancedVerify(plainText, hash);
        }
    }
}
