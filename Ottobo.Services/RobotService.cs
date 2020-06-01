using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class RobotService:ServiceBase<Robot>
    {
        public RobotService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties)  
            : base(logger, unitOfWork, includeProperties)
        {
        }
    }
}