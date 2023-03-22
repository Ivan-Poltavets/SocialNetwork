using System.Linq.Expressions;

namespace SocialNetwork.Core.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    public IQueryable<TEntity> Get(int pageIndex = 0, int itemCount = 10);

    public Task<TEntity?> GetByIdAsync(int id);

    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);

    public Task AddAsync(TEntity entity);

    public void Update(TEntity entity);

    public void Delete(TEntity entity);
}
