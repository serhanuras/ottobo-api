using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class OrderType:IEntity
    {

        public long Id { get; set; }
        
        //[FirstLetterUppercase]
        [Required(ErrorMessage = "The field with name {0} is required.")]
        [StringLength(100)]
        public string TypeName { get; set; }


        
        public ICollection<OrderDetail> OrderDetails { get; set; }
        

    }
}
