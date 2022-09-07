using PTP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Core.Repositores
{
    public interface IRepositories<TEntity, TEntityFilter> : IDisposable where TEntity : EntityBase where TEntityFilter : EntityBaseFilter
    {
        Task<TEntity> GetById(int id);
        Task<int> Create(TEntity entity);
        
        
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity entity);

        Task Delete(object entity);
    }
}
