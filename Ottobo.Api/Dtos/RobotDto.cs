using System;

namespace Ottobo.Api.Dtos
{
    public class RobotDto: IDto
    {

        public Guid Id { get; set; }
        
        public string Name { get; set; }

       
    }
}