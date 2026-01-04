using System.Linq.Expressions;
using MIRS.Core.BaseModels;

namespace MIRS.Domain.Interfaces.ISpecifications;

public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
    Expression<Func<TEntity, object>>? OrderBy { get; }
    Expression<Func<TEntity, object>>? OrderByDesc { get; }
    IReadOnlyList<Expression<Func<TEntity, object>>> Includes { get; }


    int Skip { get; }
    int Take { get; }
    bool IsPagingEnabled { get; }
}