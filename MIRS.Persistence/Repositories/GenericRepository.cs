using Microsoft.EntityFrameworkCore;
using MIRS.Core.BaseModels;
using MIRS.Domain.Interfaces.ISpecifications;
using MIRS.Domain.Interfaces.Repositories;
using MIRS.Persistence.ApplicationDbContext;
using MIRS.Persistence.Specifications;

namespace MIRS.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ApplicationContext Context;

    public GenericRepository(ApplicationContext context)
    {
        Context = context;
    }

    public async Task<TEntity?> GetEntityBySpecAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetListBySpecAsync(
        ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    private IQueryable<TEntity> ApplySpecification( ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity>
            .GetQuery(Context.Set<TEntity>().AsQueryable(), spec);
    }
}