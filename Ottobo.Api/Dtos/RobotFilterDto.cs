using System;

namespace Ottobo.Api.Dtos
{
    public class RobotFilterDto: IFilterDto
    {

        public Guid Id { get; set; }
        
        public string Name { get; set; }

       
    }
}