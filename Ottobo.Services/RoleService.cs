using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class RoleService:ServiceBase<Role>
    {
        public RoleService(ILogger logger, IUnitOfWork unitOfWork,  string includeProperties)
            : base(logger, unitOfWork, includeProperties)
        {
        }
        
        public List<Role> Filter(
            string orderingField, DataSortType dataSortType, int page, int recordsPerPage, string roleName)
        {
            IQueryable<Role> orderQueryable = this.GetQueryable();
            
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                orderQueryable = orderQueryable
                    .Where(x => x.Name.ToUpper().Contains(roleName.ToUpper()));
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