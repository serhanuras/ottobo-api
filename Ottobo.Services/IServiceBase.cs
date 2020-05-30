using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ottobo.Entities;

namespace Ottobo.Services
{
    public interface IServiceBase<TEntity> where TEntity:class, IEntity
    {
        
        List<TEntity> Read(string includeProperties, int page = 1 , int recordsPerPage = 100);

        List<TEntity> Read(int page, int recordsPerPage, string includeProperties,
            Func<TEntity, bool> filterFunc,
            string orderBy = "", DataSortType dataSortType = DataSortType.Asc);

        TEntity Read(long id, string includeProperties="");
        
        void Create(TEntity item);

        bool Exists(long id);

        void Update(TEntity item);

        void Delete(long id);

    }
}