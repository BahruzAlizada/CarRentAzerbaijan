using CoreLayer.Entity;
using System.Linq.Expressions;

namespace Core.DataAccess;

public interface IRepositoryBase<TEntity>  where TEntity : class, IEntity, new()
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity);
    List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
    TEntity Get(Expression<Func<TEntity, bool>> filter=null);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null);
    Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression = null);
    int GetCount(Expression<Func<TEntity, bool>> expression = null);
    IQueryable<TEntity> Query();
}
