using SocialNetwork.Core.Interfaces;
using SocialNetwork.Infrastructure.Data;

namespace SocialNetwork.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        var repository = new Repository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repository);

        return repository;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
