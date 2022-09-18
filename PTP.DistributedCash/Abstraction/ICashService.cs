using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTP.DistributedCash.Abstraction
{
    public interface ICashService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T data);


    }
}
