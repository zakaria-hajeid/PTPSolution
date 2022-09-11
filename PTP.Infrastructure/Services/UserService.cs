using Microsoft.Extensions.Configuration;
using PTP.Core.Common;
using PTP.Core.Dtos;
using PTP.Core.Services;
using PTP.Infrastructure.RequestAdapterPattern;
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
        private readonly ISecurityAdapterPattern _SecurityAdapterPattern;


        public UserService(IUserProxy userProxy, IConfiguration config, ISecurityAdapterPattern SecurityAdapterPattern)

        {
            _usersClient = userProxy;
            Config = config;
            _SecurityAdapterPattern = SecurityAdapterPattern;
        }
        public async Task<ResultEntity<LoginResultDtos>> Login(LoginDtos login)
        {

            //TO decoupled between Ptp and ProxyLayer Use Adapter Design Pattern

            return await _SecurityAdapterPattern.securityLoginApiAdapter(login);
        }

        public async Task<ResultEntity<LoginResultDtos>> RefreshToken(LoginResultDtos RefreShTokenModel)
        {
            return await _SecurityAdapterPattern.securityRefreshTokenApiAdapter(RefreShTokenModel);
        }
    }
}
