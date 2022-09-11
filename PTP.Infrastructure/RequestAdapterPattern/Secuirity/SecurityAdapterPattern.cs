using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.SecurityTokenService;
using PTP.Core.Common;
using PTP.Core.Dtos;
using PTP.Proxies.Comon.DtosResult;
using PTP.Proxies.Proxies.ProxyAbstractions;
using PTP.Proxies.Proxies.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Infrastructure.RequestAdapterPattern.Secuirity
{
    public class SecurityAdapterPattern : ISecurityAdapterPattern
    {
        private readonly IUserProxy _usersClient;
        private readonly IConfiguration Config;


        public SecurityAdapterPattern(IUserProxy userProxy, IConfiguration config)

        {
            _usersClient = userProxy;
            Config = config;

        }
        public async Task<ResultEntity<LoginResultDtos>> securityLoginApiAdapter(LoginDtos login)
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

        public async Task<ResultEntity<LoginResultDtos>> securityRefreshTokenApiAdapter(LoginResultDtos RefreShTokenModel)
        {
            var Header = Config["SecurityApi:ApiKey"];
            LoginResultDtos TokenModel = new LoginResultDtos()
            {
                Token = RefreShTokenModel.Token,
                RefreshToken = RefreShTokenModel.RefreshToken
            };
            var result = (await _usersClient.RefreshToken(Header, TokenModel));
            if (result is null)
            {
                throw new  BadRequestException();
            }
            ResultEntity<LoginResultDtos> resultEntity = new ResultEntity<LoginResultDtos>(result.IsSuccess,
                         result.Message, result.Payload, result.StatusCode);

            return resultEntity;
        }
    }
}
