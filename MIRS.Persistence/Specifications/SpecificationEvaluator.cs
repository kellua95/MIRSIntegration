using Microsoft.EntityFrameworkCore;
using MIRS.Core.BaseModels;
using MIRS.Domain.Interfaces.ISpecifications;

namespace MIRS.Persistence.Specifications;

public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecifications<TEntity> spec)
    {
        var Query = query;
        if (spec.Where != null)
        {
            query = query.Where(spec.Where);
        }

       /*if (spec.OrderBy != null)
        {
            Query = querys.OrderBy(spec.OrderBy);
        }*/

        if (spec.OrderByDesc != null)
        {
            Query = query.OrderByDescending(spec.OrderByDesc);
        }

        if (spec.IsPaginated != null)
        {
            Query = query.Skip(spec.Skip).Take(spec.Take);
        }
        
        if (spec.Includes != null && spec.Includes.Any())
            Query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return Query;
    }
    
}