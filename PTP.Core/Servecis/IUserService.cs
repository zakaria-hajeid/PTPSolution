using PTP.Core.Common;
using PTP.Core.Dtos;
using PTP.Proxies.Comon.DtosResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Core.Services
{
    public interface IUserService { 
        Task<ResultEntity<LoginResultDtos>> Login(LoginDtos login);
    }
}
