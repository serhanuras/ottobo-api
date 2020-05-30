using System.Collections.Generic;

namespace Ottobo.Api.Dtos
{
    public class RobotTaskDto:IDto
    {
        
        public long Id { get; set; }
        
        public RobotDto Robot { get; set; }

        public ICollection<TaskOrderDto> TaskOrders { get; set; }
    }
}