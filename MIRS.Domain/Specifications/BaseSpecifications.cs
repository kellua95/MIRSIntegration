using System.Linq.Expressions;
using MIRS.Core.BaseModels;
using MIRS.Domain.Interfaces.ISpecifications;

namespace MIRS.Domain.Specifications;

public abstract class BaseSpecifications<TEntity>: ISpecifications<TEntity> where TEntity : BaseEntity
{
  public Expression<Func<TEntity, object>> OrderBy { get; private  set; }
  public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }
  public Expression<Func<TEntity, bool>> Where { get; private set; }
  public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
  public int Take { get; private set; }
  public int Skip { get; private set;}
  public bool IsPaginated { get; private set;}

  protected BaseSpecifications() {}
  
  protected BaseSpecifications(Expression<Func<TEntity, bool>> where)
  {
    Where = where;
  }

  protected void AddInclude(Expression<Func<TEntity, object>> include)
  {
    Includes.Add(include);
  }
  
  /*protected void AddOrderBy(Expression<Func<TEntity, object>> orderBy)
  {
    OrderBy = orderBy;
  }*/

  protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderBy)
  {
    OrderByDesc = orderBy;
  }

  protected void AddPaginated(int skip, int take, bool isPaginated)
  {
    Skip = skip;
    Take = take;
    IsPaginated  = true;

  }


}