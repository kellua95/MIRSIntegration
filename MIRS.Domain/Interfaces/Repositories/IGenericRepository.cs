using System.Linq.Expressions;
using MIRS.Core.BaseModels;
using MIRS.Domain.Interfaces.ISpecifications;
using MIRS.Domain.Models;

namespace MIRS.Domain.Interfaces.Repositories;

public interface IGenericRepository<TEntity>  where TEntity : class
{
    Task<List<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> Predicate);
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<bool> DeleteByIdAsync(int id);

}