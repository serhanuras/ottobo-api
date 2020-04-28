using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class OrderDetailFilterDto: IFilterDto
    {

        public long Id { get; set; }

        public Stock Stock { get; set; }

        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }

        public OrderType OrderType { get; set; }
        
        public long OrderTypeId { get; set; }
        
        public long StockId { get; set; }

    }
}
