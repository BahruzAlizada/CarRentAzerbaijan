using CoreLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
       where TEntity : class, IEntity, new()
       where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using var context = new TContext();
            var addEntity = context.Entry(entity);
            addEntity.State = EntityState.Added;

            context.SaveChanges();
        }

        public async Task AddAsync(TEntity entity)
        {
            using var context = new TContext();
            var addEntity = context.Entry(entity);
            addEntity.State = EntityState.Added;

            await context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            using var context = new TContext();
            var deleteEntity = context.Entry(entity);
            deleteEntity.State = EntityState.Deleted;

            context.SaveChanges();
        }

        public TEntity Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter=null)
        {
            using var context = new TContext();
            return filter == null
                ? context.Set<TEntity>().FirstOrDefault()
                : context.Set<TEntity>().FirstOrDefault(filter);
        }

        public List<TEntity> GetAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();
            return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();
            return filter == null
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();
            return filter == null
                ? await context.Set<TEntity>().FirstOrDefaultAsync()
                : await context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            using var context = new TContext();

            if (expression == null)
            {
                return await context.Set<TEntity>().CountAsync();
            }
            else
            {
                return await context.Set<TEntity>().CountAsync(expression);
            }
        }

        public int GetCount(Expression<Func<TEntity, bool>> expression = null)
        {
            using var context = new TContext();

            return expression == null ? context.Set<TEntity>().Count() : context.Set<TEntity>().Count(expression);
        }

        public void Update(TEntity entity)
        {
            using var context = new TContext();
            var updateEntity = context.Entry(entity);
            updateEntity.State = EntityState.Modified;

            context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using var context = new TContext();
            var entityUpdate = context.Entry(entity);
            entityUpdate.State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public IQueryable<TEntity> Query()
        {
            using var context = new TContext();
            return context.Set<TEntity>();
        }
    }
}
