using PTP.Core.Common;
using PTP.Core.Dtos;
using PTP.Proxies.Comon.DtosResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Infrastructure.RequestAdapterPattern
{
    public interface ISecurityAdapterPattern
    {
        Task<ResultEntity<LoginResultDtos>> securityLoginApiAdapter(LoginDtos login);
    }
}
