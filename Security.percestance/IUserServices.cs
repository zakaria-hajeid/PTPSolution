using PTP.Proxies.Comon.DtosResult;
using PTP.Proxies.Proxies.Request;
using Security.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Security.percestance
{
    public interface IUserServices
    {
        Task<int> CreatUser(CreateUserDto user);
        Task<LoginResult> login(LoginRequest user);
        Task<LoginResult> RefreshToken(LoginResultDtos TokenModel);

    }
}
