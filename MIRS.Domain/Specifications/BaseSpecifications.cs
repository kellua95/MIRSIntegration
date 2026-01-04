using System.Linq.Expressions;
using MIRS.Domain.Interfaces.ISpecifications;

namespace MIRS.Domain.Specifications;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>  where TEntity : class
{
    public Expression<Func<TEntity, bool>>? Criteria { get; protected set; }
    public Expression<Func<TEntity, object>>? OrderBy { get; protected set; }
    public Expression<Func<TEntity, object>>? OrderByDesc { get; protected set; }

    private readonly List<Expression<Func<TEntity, object>>> _includes = new();
    public IReadOnlyList<Expression<Func<TEntity, object>>> Includes => _includes;

 
    public int Skip { get; protected set; }
    public int Take { get; protected set; }
    public bool IsPagingEnabled { get; protected set; }

    protected BaseSpecification() { }

    protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Expression<Func<TEntity, object>> include)
    {
        _includes.Add(include);
    }

    protected void ApplyOrderByDesc(Expression<Func<TEntity, object>> orderByDesc)
    {
        OrderByDesc = orderByDesc;
    }
    
    protected void ApplyOrder(Expression<Func<TEntity, object>> orderBy)
    {
        OrderBy = orderBy;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}