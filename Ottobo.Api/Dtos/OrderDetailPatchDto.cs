namespace Ottobo.Api.Dtos
{
    public class OrderDetailPatchDto : IPatchDto
    {

        public long OrderId { get; set;}
        
        public long MasterDataId { get; set; }

        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }

        public long OrderTypeId { get; set; }

        

    }
}
