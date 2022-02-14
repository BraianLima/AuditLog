using AuditLog.Core.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuditLog.Core.Domain.Utils
{
    public static class TokenJwt
    {
        public readonly static byte[] Key = Encoding.ASCII.GetBytes("VmYp3s6v9y$B&E)H@McQfTjWnZr4t7w!");

        public static string GenerateToken(User user)
        {
            var tokenHandler = CreateJwtSecurityTokenHandler();
            var tokenDescriptor = CreateSecurityTokenDescriptor(user);

            var token = CreateToken(tokenDescriptor, tokenHandler);
            return WriteToken(token, tokenHandler);
        }

        private static JwtSecurityTokenHandler CreateJwtSecurityTokenHandler()
        {
            return new JwtSecurityTokenHandler();
        }

        private static SecurityTokenDescriptor CreateSecurityTokenDescriptor(User user)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(ReturnListClaim(user)),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private static List<Claim> ReturnListClaim(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
        }

        private static SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor, JwtSecurityTokenHandler tokenHandler)
        {
            return tokenHandler.CreateToken(tokenDescriptor);
        }

        private static string WriteToken(SecurityToken token, JwtSecurityTokenHandler tokenHandler)
        {
            return tokenHandler.WriteToken(token);
        }

    }
}
