using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;
using Ottobo.Infrastructure.Exceptions;

namespace Ottobo.Services
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class, IEntityBase
    {

        private string _includeProperties;
        
        private readonly ILogger _logger;
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceBase(
            ILogger logger,
            IUnitOfWork unitOfWork,
            string includeProperties)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
            _includeProperties = includeProperties;
        }


        public List<TEntity> Read(int page = 1, int recordsPerPage = 100)
        {
            return _repository.GetAll(null,
                null, this._includeProperties, page, recordsPerPage).ToList();
        }

        public List<TEntity> Read(int page, int recordsPerPage,
            IQueryable<TEntity> query,
            string orderBy = "", DataSortType dataSortType = DataSortType.Asc)
        {
            if (query == null)
            {
                throw new ArgumentNullException("Filter parameter can not be null");
            }

            
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                try
                {

                    IQueryable<Entities.MasterData> masterDatasQueryable =
                        _unitOfWork.GetRepository<Entities.MasterData>().Queryable();

                    query = query.OrderBy(
                        $"{orderBy} {(dataSortType == DataSortType.Asc ? "ascending" : "descending")}");
                }
                catch
                {
                    _logger.LogWarning("Could not order by field: " + orderBy);
                }
            }

            return _repository.GetAll(query, this._includeProperties, page, recordsPerPage).ToList();
        }

        public List<TEntity> Read(
            Expression<Func<TEntity, bool>> filterExpression)
        {
            if (filterExpression == null)
            {
                throw new ArgumentNullException("Filter parameter can not be null");
            }

            return _repository.GetAll(filterExpression,
                null, this._includeProperties, 1, 100).ToList();
        }

       

        public TEntity Read(Guid id)
        {
            if (!_repository.Exists(id))
                throw new NotFoundException();

            return _repository.Get(id, this._includeProperties);
        }

        public bool Exists(Guid id)
        {
            return _repository.Exists(id);
        }


        public IQueryable<TEntity> GetQueryable()
        {
            return _unitOfWork.GetRepository<TEntity>().Queryable();
        }


        public TEntity Create(TEntity item)
        {
            item = _repository.Add(item);
            item.CreatedOn = DateTime.UtcNow;
            _unitOfWork.Save();

            return this.Read(item.Id);
        }

        public void Update(TEntity item)
        {
            if (!_repository.Exists(item.Id))
                throw new NotFoundException();

            item.UpdatedOn = DateTime.UtcNow;
            _repository.Update(item);
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            if (!_repository.Exists(id))
                throw new NotFoundException();

            _repository.Remove(id);

            _unitOfWork.Save();
        }
    }

    public enum DataSortType
    {
        Asc = 1,
        Desc = 2
    }
}