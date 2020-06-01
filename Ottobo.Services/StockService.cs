using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class StockService:ServiceBase<Stock>
    {
        public StockService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties)
            : base(logger, unitOfWork, includeProperties)
        {
        }
    }
}