using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class Stock : EntityBase
    {


        public int Quantity { get; set; }

        public DateTime LastMovementDate { get; set; }

        public string LocationLevel { get; set; }
        
        
        public MasterData MasterData { get; set; }
        
        public Guid MasterDataId { get; set; }
        
        public StockType StockType { get; set; }
        
        public Guid StockTypeId { get; set; }
        
        public Location Location { get; set; }
        
        public Guid LocationId { get; set; }
        
        public ICollection<OrderDetail> OrderDetailList { get; set; }
    }
}