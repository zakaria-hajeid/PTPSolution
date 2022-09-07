using Microsoft.Extensions.Configuration;
using PTP.Core.Common;
using PTP.Core.Dtos;
using PTP.Core.Services;
using PTP.Proxies.Comon.DtosResult;
using PTP.Proxies.Proxies.ProxyAbstractions;
using PTP.Proxies.Proxies.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserProxy _usersClient;
        private readonly IConfiguration Config;


        public UserService(IUserProxy userProxy, IConfiguration config)

        {
            _usersClient = userProxy;
            Config = config;

        }
        public async Task<ResultEntity<LoginResultDtos>> Login(LoginDtos login)
        {
            var Header = Config["SecurityApi:ApiKey"];
            LoginRequest loginRequestBodey = new LoginRequest()
            {
                username = login.Username,
                Pssword = login.Password
            };
            var result = (await _usersClient.login(Header, loginRequestBodey));
            ResultEntity<LoginResultDtos> resultEntity = new ResultEntity<LoginResultDtos>(result.IsSuccess,
               result.Message, result.Payload, result.StatusCode);

            return resultEntity;
        }
    }
}
