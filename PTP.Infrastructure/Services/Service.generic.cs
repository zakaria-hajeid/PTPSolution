using Microsoft.Extensions.Logging;
using PTP.Core.Common.Enums;
using PTP.Core.Entities;
using PTP.Core.Repositores;
using PTP.Core.Servecis;
using PTP.Data.SQL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Infrastructure.Services
{
    public  class Service<TEntity, TEntityFilter> : Services, IServices<TEntity, TEntityFilter> where TEntity : EntityBase where TEntityFilter : EntityBaseFilter
    {
        protected IRepositories<TEntity, TEntityFilter> Repository { get; }

        private readonly IRepositories<TEntity, TEntityFilter> _Repositor;
        private readonly ILogger<TEntity> _loger;
        protected Service(IUnitOfWork<DbContextTest> unitOfWork, IRepositories<TEntity, TEntityFilter> repositor,
            ILogger<TEntity> logger
            ) : base(unitOfWork)
        {
            _Repositor = repositor;
            _loger = logger;
        }

        public virtual async Task<int> Create(TEntity entity)
        {

            TEntity entityTocreat = await _Repositor.GetById(entity.Id);
            if (entityTocreat == null)
            {
                int id = await executeAsyncone(
                      async () =>
                      {
                          return await _Repositor.Create(entity);
                      });
                return id != 0 ? (int)AddEntityStatus.Added : (int)AddEntityStatus.failer;

            }
            else
                return (int)AddEntityStatus.DublicatedEntity;


        }

        public virtual async Task Delete(int id)
        {
            try
            {
                await _Repositor.Delete(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public  void Dispose()
        {
            _Repositor.Dispose();
        }

        public virtual async Task<TEntity> getByID(int id)
        {

            TEntity entity = await _Repositor.GetById(id);

            if (entity == null)
            {
                throw new Exception();
            }
            return entity;

        }

        public virtual async Task Update(TEntity entity)
        {
            try
            {
                await _Repositor.   Update(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
