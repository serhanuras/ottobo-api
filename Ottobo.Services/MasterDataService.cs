using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class MasterDataService:ServiceBase<MasterData>
    {
        public MasterDataService(ILogger logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
        {
        }
    }
}