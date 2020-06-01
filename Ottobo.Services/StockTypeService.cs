using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class StockTypeService:ServiceBase<StockType>
    {
        public StockTypeService(ILogger logger, IUnitOfWork unitOfWork,  string includeProperties)
            : base(logger, unitOfWork, includeProperties)
        {
        }
    }
}