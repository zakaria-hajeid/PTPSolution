using PTP.Core.Common.Enums;
using PTP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Core.Servecis
{
    public  interface IServices<T, M> : IDisposable where T : EntityBase where M : EntityBaseFilter
    {

        Task<T> getByID(int id);
        Task<int> Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
     
    }
}
