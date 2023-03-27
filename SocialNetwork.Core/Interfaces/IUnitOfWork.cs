namespace SocialNetwork.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task SaveChangesAsync();
}
