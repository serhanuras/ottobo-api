using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class StockType : EntityBase
    {
        public string Name { get; set; }
        
        public ICollection<Stock> StockList { get; set; }
    }
}