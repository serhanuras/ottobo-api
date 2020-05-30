using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;
using Ottobo.Infrastructure.Exceptions;

namespace Ottobo.Services
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class, IEntity
    {
        private readonly ILogger _logger;
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceBase(
            ILogger logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
        }


        public List<TEntity> Read(string includeProperties, int page = 1, int recordsPerPage = 100)
        {
            return _repository.GetAll(null,
                null, includeProperties, page, recordsPerPage).ToList();
        }

        public List<TEntity> Read(int page, int recordsPerPage, string includeProperties,
            Func<TEntity, bool> filterFunc,
            string orderBy = "", DataSortType dataSortType = DataSortType.Asc)
        {
            if (filterFunc == null)
            {
                throw new ArgumentNullException("Filter parameter can not be null");
            }

            IQueryable<TEntity> iQueryable = null;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                try
                {
                    iQueryable =
                        _unitOfWork.GetRepository<TEntity>().Queryable();

                    IQueryable<Entities.MasterData> masterDatasQueryable =
                        _unitOfWork.GetRepository<Entities.MasterData>().Queryable();

                    iQueryable = iQueryable.OrderBy(
                        $"{orderBy} {(dataSortType == DataSortType.Asc ? "ascending" : "descending")}");
                }
                catch
                {
                    _logger.LogWarning("Could not order by field: " + orderBy);
                }
            }

            Expression<Func<TEntity, bool>> filterExpression = s => filterFunc(s);

            return _repository.GetAll(filterExpression,
                null, includeProperties, page, recordsPerPage).ToList();
        }

        public TEntity Read(long id, string includeProperties = "")
        {
            if (!_repository.Exists(id))
                throw new NotFoundException();

            return _repository.Get(id, includeProperties);
        }
        
        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }


        public void Create(TEntity item)
        {
            item = _repository.Add(item);
            _unitOfWork.Save();
        }

        public void Update(TEntity item)
        {
            if (!_repository.Exists(item.Id))
                throw new NotFoundException();

            _repository.Update(item);
            _unitOfWork.Save();
        }

        public void Delete(long id)
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