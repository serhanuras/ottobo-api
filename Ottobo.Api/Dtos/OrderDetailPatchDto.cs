using System;

namespace Ottobo.Api.Dtos
{
    public class OrderDetailPatchDto : IPatchDto
    {

        public long StockId { get; set; }
        
        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }
        
        public Guid OrderId { get; set;}
        

    }
}
