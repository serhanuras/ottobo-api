
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ottobo.Entities;


namespace Ottobo.Data.Provider.IRepository
{
    public interface IRepository<T> 
        where T : class, IEntity
    {
        T  Get(long id, string includeProperties);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int page = 1,
            int recordPerPage = 100);

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
        );

        IEnumerable<T> GetAll(
            IQueryable<T> query,
            string includeProperties = null,
            int page = 1,
            int recordPerPage = 100);

        IQueryable<T> Queryable();
        
        
        T  Add(T entity);
        
        T Update(T entity);

        T Remove(long id);

        bool Exists(long id);
        
        T Remove(T entity);
    }
}