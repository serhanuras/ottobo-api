
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ottobo.Entities;


namespace Ottobo.Infrastructure.Data.IRepository
{
    public interface IRepository<T> 
        where T : class, IEntity
    {
        T  Get(long id, string includeProperties);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            string includeProperties,
            int page,
            int recordPerPage);

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter,
            string includeProperties
        );

        IEnumerable<T> GetAll(
            IQueryable<T> query,
            string includeProperties,
            int page,
            int recordPerPage);

        IQueryable<T> Queryable();
        
        
        T  Add(T entity);
        
        T Update(T entity);

        T Remove(long id);

        bool Exists(long id);
        
        T Remove(T entity);
    }
}