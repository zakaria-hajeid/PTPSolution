using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PTP.Core.Common;
using PTP.Core.Common.Attributes;
using PTP.Core.Common.Enums;
using PTP.Proxies.Comon.DtosResult;
using PTP.Proxies.Proxies.Request;
using Security.Core.Context;
using Security.Core.Dtos;
using Security.Core.Entities;
using Security.percestance;
using System.Threading.Tasks;

namespace Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ServiceFilter(typeof(SecurityValidToken))]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly IUserServices _userServices;
        private readonly ITokenService tokenServices;

        public AuthController(UserManager<Users> userManager, IUserServices userServices, ITokenService _tokenServices)
        {
            _userManager = userManager;
            tokenServices = _tokenServices;
            _userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResultEntity<int>>> Register(CreateUserDto user)
        {
            int userToCreate = await _userServices.CreatUser(user);
            if (userToCreate != (int)AddEntityStatus.failer)
                return Ok(ResultEntity<int>.Succeeded("The User Has been Addde", userToCreate));

            else
                return Ok(ResultEntity<int>.Failed("falid to added", (int)AddEntityStatus.failer));

        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResultEntity<LoginResultDtos>>> Login(LoginRequest user)
        {
            LoginResult Token = await _userServices.login(user);

                if (Token != null)
                return Ok(ResultEntity<LoginResultDtos>.Succeeded("", new LoginResultDtos() { Token=Token.Token,RefreshToken= Token .RefreshToken}));
            else

                return Ok(ResultEntity<string>.Failed("falid to CreateToken", ""));

        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<ResultEntity<LoginResultDtos>>> RefreshToken(LoginResultDtos TokenRefreshModel)
        {
            LoginResult Token = await _userServices.RefreshToken(TokenRefreshModel);

            if (Token != null)
                return Ok(ResultEntity<LoginResultDtos>.Succeeded("", new LoginResultDtos() { Token = Token.Token, RefreshToken = Token.RefreshToken }));
            else

                return Ok(ResultEntity<string>.Failed("falid to CreateToken", ""));

        }

    }
}

