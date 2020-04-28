namespace Ottobo.Api.Dtos
{
    public class OrderDetailCreationDto : ICreationDto
    {

        public long OrderId { get; set;}
        
        public long StockId { get; set; }

        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }

        public long OrderTypeId { get; set; }

        

    }
}
