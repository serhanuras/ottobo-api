using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class OrderCreationDto
    {



        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int TownId { get; set; }

        public List<OrderDetailCreationDto> OrderDetails { get; set; }

    }
}
