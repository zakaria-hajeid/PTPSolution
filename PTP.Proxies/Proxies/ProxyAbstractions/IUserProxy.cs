using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PTP.Infrastructure.Proxies.Request;
using PTP.Infrastructure.Proxies.Response;
using PTP.Proxies.Comon.DtosResult;
using PTP.Proxies.Proxies.Request;
using Refit;

namespace PTP.Proxies.Proxies.ProxyAbstractions
{
    public interface IUserProxy
    {
        [Post("/Auth/register")]
        Task<ResultEntity<int>> CreateUser([Header("SecurityApiKey")] string ApiKey, [Body] CreateUserRequest user);

        [Get("/users")]
        Task<IEnumerable<CreatUserResponse>> GetAll();


        [Post("/Auth/Login")]
        Task<ResultEntity<LoginResultDtos>> login([Header("SecurityApiKey")] string ApiKey, [Body] LoginRequest user);

    }
}
