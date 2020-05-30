using System.Collections.Generic;

namespace Ottobo.Api.Dtos
{
    public class RobotTaskCreationDto:ICreationDto
    {
        public long Id { get; set; }
        
        public RobotDto Robot { get; set; }

        public ICollection<TaskOrderDto> TaskOrders { get; set; }
    }
}