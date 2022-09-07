using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Security.Core.Dtos;
using Security.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Security.percestance.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }
       

        public Task<LoginResult> CreateJwtTokenn(Users loginInformation, IList<string> Roles)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,loginInformation.UserName),
                new Claim(ClaimTypes.Name,loginInformation.UserName)

            };
            foreach (var role in Roles)
            { 
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(Convert.ToDouble(_config["Jwt:ExpiresDayes"])),
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokens = tokenHandler.WriteToken(token);
            return Task.FromResult(new LoginResult()
            {
                Token = tokens
            });
        }
    }
}
