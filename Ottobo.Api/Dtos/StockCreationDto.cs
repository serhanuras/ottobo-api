using System;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class StockCreationDto : ICreationDto
    {
        public long LocationId { get; set; }

        public string LocationNumber { get; set; }

        public int Quantity { get; set; }

        public int StockTypeId { get; set; }

        public DateTime LastMovementDate { get; set; }

        public string LocationLevel { get; set; }

        public long MasterDataId { get; set; }
    }
}