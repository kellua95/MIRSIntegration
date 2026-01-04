using MIRS.Domain.Interfaces.DomainServices;
using MIRS.Domain.Interfaces.Repositories;
using MIRS.Domain.Models;
using MIRS.Domain.Specifications;

namespace MIRS.Domain.Services;

public class TestManager<TEntity> : ITestManager<TEntity>
    where TEntity : Test
{
    private readonly IGenericRepository<TEntity> _repository;

    public TestManager(IGenericRepository<TEntity> repository)
    {
        _repository = repository;
    }

    #region Read

    public async Task<IReadOnlyList<TEntity>> GetAllTestsAsync()
    {
        var spec = new AllTestsSpecification<TEntity>();
        return await _repository.GetListBySpecAsync(spec);
    }

    public async Task<TEntity?> GetTestByIdAsync(int id)
    {
        if (id <= 0)
            return null;

        var spec = new TestByIdSpecification<TEntity>(id);
        return await _repository.GetEntityBySpecAsync(spec);
    }

    #endregion

    #region Create

    public async Task<TEntity?> CreateAsync(TEntity entity)
    {
        if (entity == null)
            return null;

        await _repository.AddAsync(entity);
        return entity;
    }

    #endregion

    #region Update

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        if (entity == null || entity.Id <= 0)
            return null;

        _repository.Update(entity);
        return entity;
    }

    #endregion

    #region Delete

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            return false;

        var spec = new TestByIdSpecification<TEntity>(id);
        var entity = await _repository.GetEntityBySpecAsync(spec);

        if (entity == null)
            return false;

        _repository.Delete(entity);
        return true;
    }

    #endregion
}


public sealed class TestByIdSpecification<TEntity> : BaseSpecification<TEntity>
    where TEntity : Test
{
    public TestByIdSpecification(int id)
        : base(t => t.Id == id)
    {
    }
}

public sealed class AllTestsSpecification<TEntity> : BaseSpecification<TEntity>
    where TEntity : Test
{
    public AllTestsSpecification()
    {
    }
}
