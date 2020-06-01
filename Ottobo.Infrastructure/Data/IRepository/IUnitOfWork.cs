using System;
using Ottobo.Entities;

namespace Ottobo.Infrastructure.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {

        void Save();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntityBase;
    }
}