using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class OrderDetailCreationDto
    {

        public long OrderId { get; set;}
        
        public long StockId { get; set; }

        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }

        public int OrderTypeId { get; set; }

        

    }
}
