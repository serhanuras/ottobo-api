using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class OrderType:EntityBase
    {
        
        
        public string Name { get; set; }
        
        public ICollection<Order> OrderList { get; set; }

    }
}
