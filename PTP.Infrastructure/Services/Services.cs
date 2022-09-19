using PTP.Core.Common;
using PTP.Core.Repositores;
using PTP.Core.Servecis;
using PTP.Data.SQL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Infrastructure.Services
{
    public class Services : IServices
    {

        public IUnitOfWork<DbContextTest> UnitOfWork { get; set; }

        public Services(IUnitOfWork<DbContextTest> unitOfWork)
        {
            UnitOfWork = unitOfWork;

        }
        public async Task<ResultEntity<T>> executeAsync<T>(Func<Task<ResultEntity<T>>> func)
        {

            ResultEntity<T> result = null;
            var transaction = await UnitOfWork.BeginTransaction();

            result = await func();

            if (result.IsSuccess)
            {
                await UnitOfWork.CommitTransaction(transaction);
            }
            else
            {
                await UnitOfWork.RollbackTransaction(transaction);
            }

            return result;

        }

        public async Task<T> executeAsyncone<T>(Func<Task<T>> func)
        {
            var transaction = await UnitOfWork.BeginTransaction();
            try
            {
                T result = await func();
                await UnitOfWork.CommitAsync();
                await UnitOfWork.CommitTransaction(transaction);
                return result;
            }

            catch (Exception ex)
            {
                //await UnitOfWork.RollbackTransaction(transaction);
                //go to log file 
                throw;

            }



        }

    }
}

