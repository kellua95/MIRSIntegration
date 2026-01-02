using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MIRS.Core.BaseModels;
using MIRS.Domain.Interfaces.ISpecifications;
using MIRS.Domain.Interfaces.Repositories;
using MIRS.Persistence.ApplicationDbContext;

namespace MIRS.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly ApplicationContext _context;

    public  GenericRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public virtual async Task<List<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> Predicate)
    {
        try
        {
            List<TEntity>? data = await _context.Set<TEntity>().Where(Predicate).ToListAsync();
            return  data.ToList();

        }
        catch (Exception ex)
        {
            return  null;
        }
    }


    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        try
        {
            var result = await _context.Set<TEntity>().ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
  
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            dynamic? data = null;
            if (entity != null)
            {
                await _context.Set<TEntity>().AddAsync(entity);
                if (await _context.SaveChangesAsync() > 0)
                    data = new List<TEntity> { entity };
            }
            return data;
        }
        catch(Exception exception)
        {
            return null;
        }
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        try
        {
            if (entity == null)
                return null;

            _context.Set<TEntity>().Update(entity);

            var affected = await _context.SaveChangesAsync();
            return affected > 0 ? entity : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
     
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null) return false;

        _context.Set<TEntity>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
    

}
