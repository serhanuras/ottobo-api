using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ottobo.Entities
{
    public class OrderDetail :EntityBase
    {
        
        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }
        
        public int BasketId { get; set; }
        
        public Stock Stock { get; set; }

        public Guid StockId { get; set; }
        
        public Order Order { get; set; }
        
        public Guid OrderId { get; set; }
        
        
        public RobotTask  RobotTask { get; set; }
        
        public Guid RobotTaskId { get; set; }

    }
}
