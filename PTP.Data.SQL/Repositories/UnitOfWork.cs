using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTP.Core.Repositores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PTP.Data.SQL.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {
        public T Context { get; set; }


        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            return await Context.SaveChangesAsync();
        }
        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            if (_currentTransaction != null) return null;
            _currentTransaction = await Context.Database.BeginTransactionAsync();
            return _currentTransaction;
        }
        public async Task CommitTransaction(IDbContextTransaction transaction)
        {
            await transaction.CommitAsync();
        }
        public async Task RollbackTransaction(IDbContextTransaction transaction)
        {
            await transaction.RollbackAsync();
        }

        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        bool IUnitOfWork<T>.HasActiveTransaction()
        {
            return _currentTransaction != null;
        }

        public bool HasActiveTransaction => _currentTransaction != null;

        public T context
        {
            get => Context;
            set => Context = value;
        }
    }
}

