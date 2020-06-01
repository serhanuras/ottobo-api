using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class Order: EntityBase
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int TownId { get; set; }

        public OrderType OrderType { get; set; }
        
        public Guid OrderTypeId { get; set; }

        public DateTime CreationDate  {get; set;}

        public ICollection<OrderDetail> OrderDetailList   {get; set;}

    }
}
