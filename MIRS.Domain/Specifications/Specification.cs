using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MIRS.Domain.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }
    }
}
