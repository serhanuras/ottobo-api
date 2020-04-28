namespace Ottobo.Api.Dtos
{
    public class OrderDetailDto: IDto
    {

        public long Id { get; set; }

        public StockDto Stock { get; set; }

        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }

        public OrderTypeDto OrderType { get; set; }


    }
}
