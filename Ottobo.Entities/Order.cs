using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class Order: IEntity
    {

        [Key]
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int TownId { get; set; }

        public ICollection<OrderDetail>  OrderDetails { get; set; }

        public DateTime CreationDate  {get; set;}

    }
}
