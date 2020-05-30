using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class OrderDetailService:ServiceBase<OrderDetail>
    {
        public OrderDetailService(ILogger logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
        {
        }
    }
}