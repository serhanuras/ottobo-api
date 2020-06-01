using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class Location: EntityBase
    {

        public string Name { get; set; }
        
        public long MapId { get; set; }
        
        public string XCoordinate { get; set; }
        
        public string YCoordinate { get; set; }
        
        public string Theate { get; set; }
        
        public ICollection<Stock> StockList { get; set; }
    }
}