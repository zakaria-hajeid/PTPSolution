using PTP.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace PTP.Core.Servecis
{
    public interface IServices // OR CAN DO ABSTRACT CLASS
    {
        Task<T> executeAsyncone<T>(Func<Task<T>> func);
        Task<ResultEntity<T>> executeAsync<T>(Func<Task<ResultEntity<T>>> func);
    }
}
