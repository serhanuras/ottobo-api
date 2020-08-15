using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class UserService:ServiceBase<User>
    {
        public UserService(ILogger logger, IUnitOfWork unitOfWork,  string includeProperties)
            : base(logger, unitOfWork, includeProperties)
        {

            
        }
        
        public List<User> Filter(
            string orderingField, DataSortType dataSortType, int page, int recordsPerPage,
            string firstName, string lastName, string email)
        {
            IQueryable<User> orderQueryable = this.GetQueryable();
            
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                orderQueryable = orderQueryable
                    .Where(x => x.FirstName.ToUpper().Contains(firstName.ToUpper()));
            }
            
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                orderQueryable = orderQueryable
                    .Where(x => x.LastName.ToUpper().Contains(lastName.ToUpper()));
            }
            
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                orderQueryable = orderQueryable
                    .Where(x => x.Email.ToUpper().Contains(email.ToUpper()));
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