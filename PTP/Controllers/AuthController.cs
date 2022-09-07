using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTP.Core.Common;
using PTP.Core.Dtos;
using PTP.Core.Services;
using PTP.Proxies.Comon.DtosResult;
using System.Threading.Tasks;

namespace PTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ResultEntity<LoginResultDtos>>> login([FromBody] LoginDtos loginDtos)
        {
            var Response = await _userService.Login(loginDtos);
            return StatusCode(StatusCodes.Status201Created, Response);
        }
    }
}
