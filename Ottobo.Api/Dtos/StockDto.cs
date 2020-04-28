using System;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class StockDto : IDto
    {
        
        public long Id { get; set; }

        public long LocationId { get; set; }
        
        public string LocationNumber { get; set; }

        public int Quantity { get; set; }

        public StockTypeDto StockType { get; set; }

        public DateTime LastMovementDate { get; set; }

       public string LocationLevel { get; set; }

    }
}
