using PTP.Core.Entities;
using PTP.Core.Repositores;
using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace PTP.Data.SQL.Repositories
{
    public class Repository<TEntity, TEntityFilter> : IDisposable, IRepositories<TEntity, TEntityFilter> where TEntity : EntityBase where TEntityFilter : EntityBaseFilter
    {

        protected readonly DbContext _context;

        private DbSet<TEntity> Dbset
        {
            get
            {
                return _context.Set<TEntity>();

            }

        }

        public Repository(DbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(TEntity entity)
        {

            var entityId = await Dbset.AddAsync(entity);
            return entityId.Entity.Id;

        }


        public Task Delete(object id)
        {

            return Task.FromResult(Dbset.Remove(Dbset.Find(id)));

        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<TEntity> GetById(int id)
        {
            var entity = await Dbset.FindAsync(id);
            return entity;
        }

        public Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return (Task.FromResult(Dbset.Attach(entity)));
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Dbset.ToListAsync();
        }
    }
}

