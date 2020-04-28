using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class OrderDto : IDto
    {

        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int TownId { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }

    }
}
