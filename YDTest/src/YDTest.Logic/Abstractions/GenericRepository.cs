using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YDTest.Data;
using YDTest.Data.Abstractions;
using YDTest.Logic.Extensions;

namespace YDTest.Logic.Abstractions;

public class GenericRepository<TEntity> where TEntity : EntityBase
{
    internal YDTestContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(YDTestContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).NotDeleted().ToList();
        }
        else
        {
            return query.NotDeleted().ToList();
        }
    }

    public virtual async Task<TEntity> GetById(string id)
    {
        return await dbSet.AsNoTracking().NotDeleted()
            .FirstOrDefaultAsync(x=> x.Id == new Guid(id));
    }

    public virtual void Insert(TEntity entity)
    {
        dbSet.Add(entity);
    }

    public virtual async Task Delete(string id)
    {
        TEntity entityToDelete = await GetById(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}
