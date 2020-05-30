using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class OrderService:ServiceBase<Order>
    {
        public OrderService(ILogger logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
        {
        }
    }
}