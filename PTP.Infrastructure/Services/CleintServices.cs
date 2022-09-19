using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PTP.Core.Common;
using PTP.Core.Common.Enums;
using PTP.Core.Entitys;
using PTP.Core.Repositores;
using PTP.Core.Servecis;
using PTP.Data.SQL;
using PTP.Data.SQL.Repositories;
using PTP.Infrastructure.Proxies.Request;
using PTP.Proxies.Proxies.ProxyAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Infrastructure.Services
{
    public class CleintServices : Service<Cleint, CleintFilter>, ICleintServices
    {
        private readonly IClientRepository ClientRepository;
        private readonly IUserProxy _usersClient;
        private readonly IConfiguration Config;


        public CleintServices(IUnitOfWork<DbContextTest> unitOfWork, IClientRepository repositor, ILogger<Cleint> logger
            , IUserProxy userProxy, IConfiguration config)
            : base(unitOfWork, repositor, logger)
        {
            ClientRepository = repositor;
            _usersClient = userProxy;
                Config = config;

        }

        public async Task<bool> CheckDuplicateUsername(string name)
        {
            IEnumerable<Cleint> ClientUser = await ClientRepository.GetAll() ;
            return ClientUser.Where(x => x.username == name).Any();
        }
        public override async Task<int> Create(Cleint entity)
        {


            int id = await executeAsyncone(
                  async () =>
                  {
                      return await ClientRepository.Create(entity);
                  });
            return id != 0 ? (int)AddEntityStatus.Added : (int)AddEntityStatus.failer;

        }

        public async Task<ResultEntity<int>> CretaUserWithRefit(CreateUserRequest user)
        {
            var Header = Config["SecurityApi:ApiKey"];

            var result = (await _usersClient.CreateUser(Header,user));
            ResultEntity<int> resultEntity = new ResultEntity<int>(result.IsSuccess,
               result.Message, result.Payload, result.StatusCode);
            
            return resultEntity;
        }
    }
}

