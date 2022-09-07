using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PTP.Infrastructure.Utilities
{
    public static class TokenUtilites
    {

        public static string GenerateToken(List<Claim> claims, DateTime expires)
        {
           /* string generatedToken = string.Empty;

            if (!string.IsNullOrEmpty(SecretKey) && !string.IsNullOrEmpty(HashingAlgorithm) &&
                !string.IsNullOrEmpty(Issuer) && !string.IsNullOrEmpty(Audience) && claims != null && claims.Count > 0)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(SecretKey);
                var key = new SymmetricSecurityKey(bytes);
                var credentials = new SigningCredentials(key, HashingAlgorithm);

                var token = new JwtSecurityToken(
                    issuer: Issuer,
                    audience: Audience,
                    claims: claims,
                    expires: expires,
                    signingCredentials: credentials);

                var securityTokenHandler = new JwtSecurityTokenHandler();
                generatedToken = securityTokenHandler.WriteToken(token);
            }*/

            return "";
        }

    }
}
