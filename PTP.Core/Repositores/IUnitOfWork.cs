using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace PTP.Core.Repositores
{
    public interface IUnitOfWork<T> where T : DbContext
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<IDbContextTransaction> BeginTransaction();
        Task CommitTransaction(IDbContextTransaction transaction);
        Task SaveCurrentChanges();
        Task RollbackTransaction(IDbContextTransaction transaction);
        public IDbContextTransaction GetCurrentTransaction();

        public bool HasActiveTransaction();
        public T context { get; set; }

    }
}
