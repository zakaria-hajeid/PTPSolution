using PTP.Core.Common;
using PTP.Core.Entitys;
using PTP.Infrastructure.Proxies.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Core.Servecis
{
    public interface ICleintServices : IServices<Cleint, CleintFilter>
    {
        Task<bool> CheckDuplicateUsername(string name);
       Task<ResultEntity<int>> CretaUserWithRefit(CreateUserRequest user);
    }
}
