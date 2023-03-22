﻿namespace SocialNetwork.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    void SaveChanges();
    Task SaveChangesAsync();
}
