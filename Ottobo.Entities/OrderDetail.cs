using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Ottobo.Entities
{
    public class OrderDetail :IEntity
    {
        
        [Key]
        public long Id { get; set; }
        
        public Stock Stock { get; set; }
        
        public long StockId { get; set; }

        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }

        public OrderType OrderType { get; set; }
        
        public long OrderTypeId { get; set; }
        
        [JsonIgnore]
        public Order Order { get; set; }
        
        public long OrderId { get; set; }
        
        
        
        
        
        


    }
}