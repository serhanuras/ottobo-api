using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class PurchaseTypeCreationDto: ICreationDto
    {
        [Required(ErrorMessage = "The field with name {0} is required.")]
        [StringLength(100)]
        public string Name { get; set; }

    }
}
