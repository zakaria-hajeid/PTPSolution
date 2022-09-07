using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PTP.Core.Common.Enums;
using PTP.Proxies.Proxies.Request;
using Security.Application.Repository;
using Security.Core.Dtos;
using Security.Core.Entities;
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

        public userServices(IMapper mapper, IUserRepo userRepo, UserManager<Users> userManager,
            SignInManager<Users> signInManager, ITokenService tokenServices)
        {
            _mapper = mapper;
            _UserRepo = userRepo;
            _signInManager = signInManager;
            _userManager = userManager;
            this.tokenServices = tokenServices;
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

                    var TokenObj = await tokenServices.CreateJwtTokenn(appUser, Roles);
                    return new LoginResult()
                    {
                        Token = TokenObj.Token
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
