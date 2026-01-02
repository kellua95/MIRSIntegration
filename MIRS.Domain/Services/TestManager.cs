using MIRS.Core.BaseModels;
using MIRS.Domain.Interfaces.DomainServices;
using MIRS.Domain.Interfaces.Repositories;
using MIRS.Domain.Models;

namespace MIRS.Domain.Services;

public class TestManager<TEntity>:ITestManager<TEntity> where TEntity : Test
{
   private readonly IGenericRepository<TEntity>  _genericRepository;

    public TestManager(IGenericRepository<TEntity>  genericRepository )
    {
        _genericRepository = genericRepository;
    }
    
    #region  Read

    public async Task<List<TEntity>> GetAllTestsAsync()
    {
        try
        {
            var result = await _genericRepository.GetAllAsync();
            return result.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    public async Task<TEntity> GetTestsByIdAsync(int id)
    {
        try
        {
            var result = await _genericRepository.GetByCondition(x  => x.Id == id);
            return result.ToList().FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    #endregion

    #region Create
    public async Task<TEntity?> CreateAsync(TEntity entity)
    {
        try
        {
            if (entity == null)
                return null;

            var created = await _genericRepository.CreateAsync(entity);
            return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    #endregion

    #region Update
    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        try
        {
            if (entity == null || entity.Id <= 0)
                return null;

            var updated = await _genericRepository.UpdateAsync(entity);
            return updated;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    #endregion
  
    #region Delete
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            if (id <= 0)
                return false;

            await _genericRepository.DeleteByIdAsync(id);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    #endregion
}