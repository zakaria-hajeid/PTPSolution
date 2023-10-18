using Microsoft.AspNetCore.Http;
using PTP.Core.Repositores;
using PTP.Data.SQL;
using Security.Core.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Infrastructure.Middlwars
{
    public class TransactionMiddileware
    {
        private readonly RequestDelegate _next;

        public TransactionMiddileware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                DataContext DbCotextInstance = (DataContext)context.RequestServices.GetService(typeof(DataContext));
                IUnitOfWork<DataContext> _unitOfWork = (IUnitOfWork<DataContext>)context.RequestServices
                .GetService(typeof(IUnitOfWork<DataContext>));
                _unitOfWork.context = DbCotextInstance;

                if (_unitOfWork.context != null)
                {

                    if (!_unitOfWork.HasActiveTransaction())
                    {
                        await _unitOfWork.BeginTransaction();
                        await _next(context);
                        await _unitOfWork.CommitTransaction(_unitOfWork.GetCurrentTransaction());
                    }
                    else
                    {
                        await _next(context);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
