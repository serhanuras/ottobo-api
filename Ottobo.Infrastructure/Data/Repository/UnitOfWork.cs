using System.Collections.Generic;
using Ottobo.Infrastructure.Data.IRepository;
using Ottobo.Infrastructure.Data.PostgreSql;
using Ottobo.Entities;

namespace Ottobo.Infrastructure.Data.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly Dictionary<string,object> _repositories;
        private readonly ApplicationDbContext _context;
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
            //OrderType = new OrderTypeRepository(_db);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntityBase
        {
            var repositoryName = typeof(TEntity).Name;

            var repository = new Repository<TEntity>(_context);
            if (!_repositories.ContainsKey(repositoryName))
               _repositories.Add(repositoryName, repository);

            return (Repository<TEntity>) _repositories[repositoryName];
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}