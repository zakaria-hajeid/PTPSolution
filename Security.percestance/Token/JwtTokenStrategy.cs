using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PTP.Core.Repositores;
using Security.Core.Context;
using Security.Core.Dtos;
using Security.Core.Entities;
using Security.percestance.Token.TokenCreateStrategy;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Security.percestance.Token
{
    public class JwtTokenStrategy : IGenerateToken
    {
        private readonly IConfiguration _config;

        private readonly DataContext _Db;
        private readonly IUnitOfWork<DataContext> UnitOfwork;

        public JwtTokenStrategy(IConfiguration config, DataContext Db, IUnitOfWork<DataContext> UnitOfwork)
        {
            _config = config;
            _Db = Db;
            this.UnitOfwork = UnitOfwork;   
        }
        public  Task<object> CreateToken(params object[] Credentials)
        {

            Users loginInformation = (Users)Credentials[0];
            IList<string> Roles = (IList<string>)Credentials[1];
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,loginInformation.UserName),
                new Claim(ClaimTypes.Name,loginInformation.UserName)

            };
            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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
            object tokens = tokenHandler.WriteToken(token);
            var refreshToken = Credentials[2] as string;
            loginInformation.RefreshToken = refreshToken;
            loginInformation.RefreshTokenExpiryTime = DateTime.Now.AddDays(10);
            _Db.Users.Update(loginInformation);
            UnitOfwork.SaveCurrentChanges();
            return Task.FromResult( new { token = tokens , refreshToken= refreshToken } as object);
        }
    }
}
