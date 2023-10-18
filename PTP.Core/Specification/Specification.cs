using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Core.Specification
{
    public abstract class Specification<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

       // delegate Di =
        public abstract Expression<Func<T, bool>> ToExpression();


        public void ss()
        {

        }
    }
}
