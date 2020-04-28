using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class PurchaseType:IEntity
    {
         [Key]
        public long Id { get; set; }

    
        //[FirstLetterUppercase]
        [Required(ErrorMessage = "The field with name {0} is required.")]
        [StringLength(100)]
        public string TypeName { get; set; }
        
        
        public ICollection<MasterData> MasterDatas { get; set; }
        

    }
}
