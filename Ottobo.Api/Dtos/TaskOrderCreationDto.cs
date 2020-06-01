using System;

namespace Ottobo.Api.Dtos
{
    public class TaskOrderCreationDto:ICreationDto
    {

        public Guid OrderDetailId { get; set; }

        public Guid RobotTaskId { get; set; }
    }
}