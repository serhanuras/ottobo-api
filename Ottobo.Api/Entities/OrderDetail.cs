using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Entities
{
    public class OrderDetail 
    {

        [Key]
        public long Id { get; set; }

        public Stock Stock { get; set; }

        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }

        public OrderType OrderType { get; set; }

    }
}
