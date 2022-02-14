using System;
using System.Security.Cryptography;
using System.Text;

namespace AuditLog.Core.Domain.Utils
{
    public sealed class HashCryptography
    {
        private const string passwordSalt = "149A8727-19E1-4014-9E94-C5C7E82A60A9 D4443F99-2C4C-486A-BC6A-0A5614D7A92D 9EE0BE7E-995E-4ADA-8171-70B204333CC3";
        public static string Encrypt(string plainText)
        {
            using SHA512 shA512 = SHA512.Create();
            return BitConverter.ToString(shA512.ComputeHash(Encoding.ASCII.GetBytes(plainText + passwordSalt))).Replace("-", "").ToLower();
        }
    }
}
