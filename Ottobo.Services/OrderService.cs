using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class OrderService : ServiceBase<Order>
    {
        public OrderService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties)  
            : base(logger, unitOfWork, includeProperties)
        {
        }

        public List<Order> Filter(string name, int cityId, int townId, DateTime startDate, DateTime endDate,
            string orderingField, DataSortType dataSortType, int page, int recordsPerPage)
        {
            IQueryable<Order> orderQueryable = this.GetQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                orderQueryable = orderQueryable
                    .Where(x => x.Name.ToUpper().Contains(name.ToUpper()));
            }

            if (cityId != 0)
            {
                orderQueryable = orderQueryable
                    .Where(x => x.CityId == cityId);
            }

            if (townId != 0)
            {
                orderQueryable = orderQueryable
                    .Where(x => x.TownId == townId);
            }

            if (startDate.Year != 1 && endDate.Year != 1)
            {
                orderQueryable = orderQueryable
                    .Where(s => (s.Date >= startDate && s.Date <= endDate));
            }
            
            return this.Read(page,
                recordsPerPage,
                orderQueryable,
                orderingField,
                dataSortType
            );
        }
    }
}