using System;
using System.Collections.Generic;

namespace Ottobo.Api.Dtos
{
    public class RobotTaskDto:IDto
    {
        
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Guid RobotId { get; set; }
        
    }
}