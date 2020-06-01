using System;
using System.Collections.Generic;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class OrderDto : IDto
    {

        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int TownId { get; set; }
        
        public OrderTypeDto OrderType { get; set; }

    }
}
