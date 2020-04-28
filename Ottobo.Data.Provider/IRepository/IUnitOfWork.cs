using System;
using Ottobo.Entities;

namespace Ottobo.Data.Provider.IRepository
{
    public interface IUnitOfWork : IDisposable
    {

        void Save();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
    }
}