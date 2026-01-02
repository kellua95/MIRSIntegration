using MIRS.Domain.Models;

namespace MIRS.Domain.Interfaces.DomainServices;

public interface ITestManager<TEntity> where TEntity : Test
{
    Task<List<TEntity>> GetAllTestsAsync();
    Task<TEntity> GetTestsByIdAsync(int id);
    Task<TEntity?> CreateAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
}