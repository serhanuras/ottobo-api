using System;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class OrderDetailFilterDto: IFilterDto
    {

        public long Id { get; set; }

        public long StockId { get; set; }
        
        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }
        
        public Guid OrderId { get; set;}

        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; }
    }
}
