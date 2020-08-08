using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ottobo.Entities;

namespace Ottobo.Services
{
    public interface IServiceBase<TEntity> where TEntity:class, IEntityBase
    {

        List<TEntity> Read(int page = 1 , int recordsPerPage = 100);

        List<TEntity> Read(int page, int recordsPerPage,
            IQueryable<TEntity> query,
            string orderBy = "", DataSortType dataSortType = DataSortType.Asc);

        TEntity Read(Guid id);

        List<TEntity> Read(
            Expression<Func<TEntity, bool>> filterExpression);

        IQueryable<TEntity> GetQueryable();

        TEntity Create(TEntity item);

        bool Exists(Guid id);
        
        long Count();

        void Update(TEntity item);

        void Delete(Guid id);

    }
}