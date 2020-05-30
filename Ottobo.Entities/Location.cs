using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class Location: IEntity
    {
        [Key]
        public long Id { get; set; }
        
        public long MapId { get; set; }
        
        public string XCoordinate { get; set; }
        
        public string YCoordinate { get; set; }
        
        public string Theate { get; set; }
    }
}