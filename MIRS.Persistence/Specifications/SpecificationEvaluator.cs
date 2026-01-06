using Microsoft.EntityFrameworkCore;
using MIRS.Domain.Interfaces.ISpecifications;

namespace MIRS.Persistence.Specifications;

public static class SpecificationEvaluator<TEntity>
    where TEntity : class
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity>? spec)
    {
        IQueryable<TEntity> query = inputQuery;

        if (spec == null) return query;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.OrderByDesc != null)
        {
            query = query.OrderByDescending(spec.OrderByDesc);
        }
        else if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        if (spec.Includes != null)
        {
            query = spec.Includes.Aggregate(
                query,
                (current, include) => current.Include(include));
        }

        return query;
    }
}