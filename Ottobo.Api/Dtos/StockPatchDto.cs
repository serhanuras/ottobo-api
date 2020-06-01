using System;

namespace Ottobo.Api.Dtos
{
    public class StockPatchDto : IPatchDto
    {

        public string LocationNumber { get; set; }

        public int Quantity { get; set; }
        
        public DateTime LastMovementDate { get; set; }

        public string LocationLevel { get; set; }
        
        public Guid MasterDataId { get; set; }
        
        public Guid StockTypeId { get; set; }
        
        public Guid LocationId { get; set; }

    }
}
