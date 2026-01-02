using System.Linq.Expressions;
using MIRS.Core.BaseModels;

namespace MIRS.Domain.Interfaces.ISpecifications;

public interface ISpecifications<TEntity> where TEntity : BaseEntity
{
    public Expression<Func<TEntity, object>> OrderBy { get; }
    public Expression<Func<TEntity, object>> OrderByDesc { get; }
    public Expression<Func<TEntity, bool>> Where { get; }
    public List<Expression<Func<TEntity, object>>> Includes { get; }
    public int Take { get; }
    public int Skip { get; }
    public bool IsPaginated { get; }
}