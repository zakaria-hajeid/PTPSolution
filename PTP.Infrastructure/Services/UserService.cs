using Microsoft.Extensions.Configuration;
using PTP.Core.Common;
using PTP.Core.Dtos;
using PTP.Core.Services;
using PTP.DistributedCash.Abstraction;
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
        private readonly ICashService CashService;


        public UserService(IUserProxy userProxy, IConfiguration config, ISecurityAdapterPattern SecurityAdapterPattern
            , ICashService CashService
            )

        {
            _usersClient = userProxy;
            Config = config;
            _SecurityAdapterPattern = SecurityAdapterPattern;
            this.CashService = CashService;
        }
        public async Task<ResultEntity<LoginResultDtos>> Login(LoginDtos login)
        {

            //TO decoupled between Ptp and ProxyLayer Use Adapter Design Pattern
            var cashResult = await CashService.GetAsync<LoginResultDtos>(login.Username);
            if (cashResult != null)
            {
                ResultEntity<LoginResultDtos> resultEntity = new ResultEntity<LoginResultDtos>(true,
                          "return from cash", cashResult, null);
                return resultEntity;
            }

            var result = await _SecurityAdapterPattern.securityLoginApiAdapter(login);
            await CashService.SetAsync<LoginResultDtos>(login.Username, result.Payload);
            return result;

        }

        public async Task<ResultEntity<LoginResultDtos>> RefreshToken(LoginResultDtos RefreShTokenModel)
        {
            return await _SecurityAdapterPattern.securityRefreshTokenApiAdapter(RefreShTokenModel);
        }
    }
}
