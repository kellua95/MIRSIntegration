using MIRS.Core.BaseModels;
using MIRS.Domain.Interfaces.Repositories;

namespace MIRS.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> CompleteAsync();
}
