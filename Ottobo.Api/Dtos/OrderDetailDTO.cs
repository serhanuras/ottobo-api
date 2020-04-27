using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class OrderDetailDto
    {

        public long Id { get; set; }

        public Stock Stock { get; set; }

        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }

        public OrderType OrderType { get; set; }

    }
}
