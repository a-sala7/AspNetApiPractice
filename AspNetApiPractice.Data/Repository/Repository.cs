using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspNetApiPractice.Data.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly Entities dbContext;
    private readonly DbSet<T> dbSet;
    public Repository(Entities dbContext)
    {
        this.dbContext = dbContext;
        dbSet = dbContext.Set<T>();
    }

    public virtual T Add(T entity)
    {
        return dbSet.Add(entity).Entity;
    }

    public virtual async Task<IEnumerable<T>> All()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> All(Expression<Func<T, bool>> predicate)
    {
        return await dbSet
            .Where(predicate)
            .ToListAsync();
    }

    public virtual void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public virtual async Task<T?> GetById(int id)
    {
        return await dbSet.FindAsync(id);
    }
}
