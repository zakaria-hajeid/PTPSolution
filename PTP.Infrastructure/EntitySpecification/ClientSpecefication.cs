using PTP.Core.Entitys;
using PTP.Core.Servecis;
using PTP.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Infrastructure.EntitySpecification
{
    public class ClientSpecefication : Specification<Cleint>
    {
        private readonly ICleintServices _cleintServices;

        public ClientSpecefication(ICleintServices cleintServices)
        {
            _cleintServices = cleintServices;
        }

        public override Expression<Func<Cleint, bool>> ToExpression()
        {
            return x =>
            Task.FromResult(
             _cleintServices.CheckDuplicateUsername(x.username).Result
               ).Result;
        }
    }
}
