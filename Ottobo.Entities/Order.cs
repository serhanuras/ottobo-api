using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class Order
    {

        [Key]
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int TownId { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public DateTime CreationDate  {get; set;}

    }
}
