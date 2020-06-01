using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class OrderTypeService:ServiceBase<OrderType>
    {
        public OrderTypeService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties) 
             : base(logger, unitOfWork, includeProperties)
        {
        }
    }
}