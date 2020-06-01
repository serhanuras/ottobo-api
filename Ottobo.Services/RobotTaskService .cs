using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class RobotTaskService:ServiceBase<RobotTask>
    {
        public RobotTaskService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties)   
            : base(logger, unitOfWork, includeProperties)
        {
        }
    }
}