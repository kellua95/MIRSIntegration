using MIRS.Domain.Models;

namespace MIRS.Domain.Interfaces.DomainServices;

public interface ITestManager<TEntity>

{
    Task<IReadOnlyList<TEntity>> GetAllTestsAsync();
    Task<TEntity?> GetTestByIdAsync(int id);

    Task<TEntity?> CreateAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
}