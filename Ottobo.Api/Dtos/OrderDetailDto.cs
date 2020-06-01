using System;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class OrderDetailDto: IDto
    {

        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }
        
        public StockDto Stock { get; set; }
        
        public OrderDto Order { get; set; }

        public RobotTaskDto RobotTask { get; set; }

    }
}
