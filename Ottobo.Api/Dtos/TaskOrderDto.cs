using System;

namespace Ottobo.Api.Dtos
{
    public class TaskOrderDto:IDto
    {
        public Guid Id { get; set; }
        
        public Guid OrderDetailId { get; set; }

        public Guid RobotTaskId { get; set; }
    }
}