using System;

namespace Ottobo.Api.Dtos
{
    public class RobotFilterDto: IFilterDto
    {

        public Guid Id { get; set; }
        
        public string Name { get; set; }


        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; }
    }
}