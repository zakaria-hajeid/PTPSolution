using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PTP.Core.Common.Enums;
using PTP.Proxies.Proxies.Request;
using Security.Application.Repository;
using Security.Core.Dtos;
using Security.Core.Entities;
using Security.percestance.Token;
using Security.percestance.Token.Enums;
using Security.percestance.Token.TokenCreateStrategy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Security.percestance.Services
{
    public class userServices : IUserServices
    {
        public readonly IMapper _mapper;
        public readonly IUserRepo _UserRepo;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly ITokenService tokenServices;
        private TokenStrategyContext tokenStrategyContext;
        private readonly IConfiguration _config;


        public userServices(IMapper mapper, IUserRepo userRepo, UserManager<Users> userManager,
            SignInManager<Users> signInManager, ITokenService tokenServices, IConfiguration config)
        {
            _mapper = mapper;
            _UserRepo = userRepo;
            _signInManager = signInManager;
            _userManager = userManager;
            this.tokenServices = tokenServices;
            _config = config;
        }
        public async Task<int> CreatUser(CreateUserDto user)
        {
            user.PhoneNumber = "00962790360186";
            user.Email = "Zakarialhajeid1998@gmail.com";
            Users userToAdd = _mapper.Map<Users>(user);
            IdentityResult userToAddResult = await _UserRepo.AddUser(userToAdd,user.Password);
            if (userToAddResult.Succeeded)
            {
                return userToAdd.Id;
            }
            else
                return (int)AddEntityStatus.failer;
        }

        private IGenerateToken TokenFactoryMethod(TokenType type)
        {
            if (type == TokenType.JWT)
                return new JwtTokenStrategy(_config);
            else
                return new JwtTokenStrategy(_config);

        }

        public async Task<LoginResult> login(LoginRequest user)
        {
            try
            {
                Users User = await _userManager.FindByNameAsync(user.username);
                 SignInResult result = await _signInManager.CheckPasswordSignInAsync(User, user.Pssword, false);

                if (result.Succeeded)
                {
                    Users appUser = await _userManager.Users.FirstOrDefaultAsync(
                      u => u.NormalizedUserName == user.username.ToUpper()

                  );
                    IList<string> Roles = await _userManager.GetRolesAsync(appUser);
                    // use the stretagy pattern to create the jwt Token
                    tokenStrategyContext = new TokenStrategyContext(TokenFactoryMethod(TokenType.JWT));
                    object token = await tokenStrategyContext.CreateToken(appUser, Roles);
                    //var TokenObj = await tokenServices.CreateJwtTokenn(appUser, Roles);
                    return new LoginResult()
                    {
                        Token = (string)token
                    };
                }
                else
                {
                    return new LoginResult()
                    {

                    };

                }
            }
           catch(Exception ex)
            {
                return null;
            }

        }
    }
}
