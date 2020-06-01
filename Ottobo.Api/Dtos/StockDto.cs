using System;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class StockDto : IDto
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public DateTime LastMovementDate { get; set; }

        public string LocationLevel { get; set; }

        public MasterDataDto MasterData { get; set; }

        public StockTypeDto StockType { get; set; }

        public LocationDto Location { get; set; }
    }
}