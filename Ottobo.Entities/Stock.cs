using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class Stock : IEntity
    {
        [Key] public long Id { get; set; }

        public long LocationId { get; set; }

        public string LocationNumber { get; set; }

        public int Quantity { get; set; }

        public StockType StockType { get; set; }

        public long StockTypeId { get; set; }

        public DateTime LastMovementDate { get; set; }

        public string LocationLevel { get; set; }

        public MasterData MasterData { get; set; }
        
        public long MasterDataId { get; set; }
    }
}