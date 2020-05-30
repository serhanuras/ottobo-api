using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class Robot: IEntity
    {
        [Key]
        public long Id { get; set; }
        
        public string Name { get; set; }
        
    }
}