using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ottobo.Infrastructure.Data.IRepository;
using Ottobo.Entities;
using Ottobo.Extensions;
using Ottobo.Infrastructure.Extensions;

namespace Ottobo.Infrastructure.Data.Repository
{
     public class Repository<T> : IRepository<T> where T : class, IEntityBase
    {
        protected readonly DbContext Context;
        internal readonly DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.DbSet = context.Set<T>();
        }

        
        public  T Add(T entity)
        {
            this.DbSet.Add(entity);

            return entity;
        }

        public T Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public T Get(Guid id, string includeProperties)
        {
            IQueryable<T> query = DbSet;
            
            // if(includeProperties != null)
            // {
            //     foreach(var includeProperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
            //     {
            //         query = query.Include(includeProperty);
            //     }
            // }
            
            if(!String.IsNullOrWhiteSpace(includeProperties))
                query = AddIncludes(query, includeProperties);

            var result = query.Where(x => x.Id == id).AsNoTracking().ToList();

            if (result.Count == 0)
                return null;
            
            return result[0];
        }

        private IQueryable<T> AddIncludes(IQueryable<T> queryable, string includeProperties)
        {
            if(includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                    
                    /*
                    if (includeProperty.Split(new char[] {'>'}, StringSplitOptions.RemoveEmptyEntries).Length == 1)
                    {
                        queryable = queryable.Include(includeProperty);
                    }
                    else
                    {
                        var subProperties =
                            includeProperty.Split(new char[] {'>'}, StringSplitOptions.RemoveEmptyEntries)[1];

                        foreach (var subIncludeProperty in subProperties.Split(new char[] { '|'}, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var mainInclude = includeProperty.Split(new char[] {'>'},
                                StringSplitOptions.RemoveEmptyEntries)[0];

                            var includePath = $"{mainInclude}.{subIncludeProperty}";
                            
                            queryable = queryable.Include(includePath);
                        }
                    }
                    */
                }
            }

            return queryable;
        }
        

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int page = 1,
            int recordPerPage = 100)
        {
            IQueryable<T> query = DbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            //include properties will be comma seperated
            // if(includeProperties != null)
            // {
            //     foreach(var includeProperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
            //     {
            //         //.Include("ApplicationsWithOverrideGroup.NestedProp") 
            //         query = query.Include(includeProperty);
            //     }
            // }

            if(!String.IsNullOrWhiteSpace(includeProperties))
                query = AddIncludes(query, includeProperties);

            query = query.Paginate(page, recordPerPage);
            
            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking().ToList();
            }
            
            return query.AsNoTracking().ToList();
        }
        
        public IEnumerable<T> GetAll(
            IQueryable<T> query,
            string includeProperties = null,
            int page = 1,
            int recordPerPage = 100)
        {
            //include properties will be comma seperated
            // if(includeProperties != null)
            // {
            //     foreach(var includeProperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
            //     {
            //         query = query.Include(includeProperty);
            //     }
            // }
            
            query = AddIncludes(query, includeProperties);

            query = query.Paginate(page, recordPerPage);
            
            return query.AsNoTracking().ToList();
        }

        public IQueryable<T> Queryable()
        {
            IQueryable<T> query = DbSet;

            return query;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return  query.FirstOrDefault();
        }

        public T Remove(Guid id)
        {
            T entityToRemove = DbSet.Find(id);
            
            if (entityToRemove == null)
            {
                return null;
            }
            
            return Remove(entityToRemove);
        }

        public bool Exists(Guid id)
        {
            return DbSet.Any(x => x.Id == id);
        }

        public T Remove(T entity)
        {
            DbSet.Remove(entity);

            return entity;
        }
        
        public long Count()
        {
            return DbSet.Count();
        }
    }
}