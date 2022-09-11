using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PTP.Core.Common.Enums;
using PTP.Core.Repositores;
using PTP.Proxies.Comon.DtosResult;
using PTP.Proxies.Proxies.Request;
using Security.Application.Repository;
using Security.Core.Context;
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
        private readonly DataContext _Db;
        private readonly IUnitOfWork<DataContext> UnitOfwork;



        public userServices(IMapper mapper, IUserRepo userRepo, UserManager<Users> userManager,
            SignInManager<Users> signInManager, ITokenService tokenServices, IConfiguration config, DataContext Db

            , IUnitOfWork<DataContext> UnitOfwork)
        {
            _mapper = mapper;
            _UserRepo = userRepo;
            _signInManager = signInManager;
            _userManager = userManager;
            this.tokenServices = tokenServices;
            _config = config;
            _Db = Db;
            this.UnitOfwork = UnitOfwork;  
        }
        public async Task<int> CreatUser(CreateUserDto user)
        {
            user.PhoneNumber = "00962790360186";
            user.Email = "Zakarialhajeid1998@gmail.com";
            Users userToAdd = _mapper.Map<Users>(user);
            IdentityResult userToAddResult = await _UserRepo.AddUser(userToAdd, user.Password);
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
                return new JwtTokenStrategy(_config, _Db, UnitOfwork);
            else
                return new JwtTokenStrategy(_config, _Db, UnitOfwork);

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

                    string refreshToken = await tokenServices.CreateRefreshToken();
                    tokenStrategyContext = new TokenStrategyContext(TokenFactoryMethod(TokenType.JWT));
                    object token = await tokenStrategyContext.CreateToken(appUser, Roles, refreshToken);
                    //var TokenObj = await tokenServices.CreateJwtTokenn(appUser, Roles);
                    return new LoginResult()
                    {
                        Token = (string)token.GetType()?.GetProperty("token")?.GetValue(token),
                        RefreshToken = (string)token.GetType()?.GetProperty("refreshToken")?.GetValue(token)

                    };
                }
                else
                {
                    return new LoginResult()
                    {

                    };

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<LoginResult> RefreshToken(LoginResultDtos TokenModel)
        {

            string accessToken = TokenModel.Token;
            string refreshToken = TokenModel.RefreshToken;
            var principal = await tokenServices.GetClaimToken(accessToken);
            var username =  principal.Identity.Name; //this is mapped to the Name claim by default
            var user = await _Db.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return null;
            IList<string> Roles = await _userManager.GetRolesAsync(user);

            tokenStrategyContext = new TokenStrategyContext(TokenFactoryMethod(TokenType.JWT));
            string refreshTokenCreate = await tokenServices.CreateRefreshToken();

            object token = await tokenStrategyContext.CreateToken(user, Roles, refreshTokenCreate);

            return new LoginResult()
            {
                Token = (string)token.GetType()?.GetProperty("token")?.GetValue(token),
                RefreshToken = (string)token.GetType()?.GetProperty("refreshToken")?.GetValue(token)

            };
        }
    }
}
