using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class LocationCreationDto: ICreationDto
    {
        
        public long MapId { get; set; }
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public string XCoordinate { get; set; }
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public string YCoordinate { get; set; }
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public string Theate { get; set; }
        
    }
}