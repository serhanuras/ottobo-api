using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class TaskOrderService:ServiceBase<TaskOrder>
    {
        public TaskOrderService(ILogger logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
        {
        }
    }
}