using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class PurchaseTypeService:ServiceBase<PurchaseType>
    {
        public PurchaseTypeService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties) 
            : base(logger, unitOfWork, includeProperties)
        {
        }
    }
}