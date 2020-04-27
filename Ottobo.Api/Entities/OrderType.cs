using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Entities
{
    public class OrderType
    {

        [Key]
        public int Id { get; set; }

        //[FirstLetterUppercase]
        [Required(ErrorMessage = "The field with name {0} is required.")]
        [StringLength(100)]
        public string TypeName { get; set; }

    }
}
