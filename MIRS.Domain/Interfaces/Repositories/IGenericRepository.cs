using System.Linq.Expressions;
using MIRS.Core.BaseModels;
using MIRS.Domain.Interfaces.ISpecifications;
using MIRS.Domain.Models;

namespace MIRS.Domain.Interfaces.Repositories;

public interface IGenericRepository<TEntity>
{
    Task<TEntity?> GetEntityBySpecAsync(ISpecification<TEntity> spec);

    Task<IReadOnlyList<TEntity>> GetListBySpecAsync(
        ISpecification<TEntity> spec);

    Task<int> CountAsync(ISpecification<TEntity> spec);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);

}