using System;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class StockCreationDto : ICreationDto
    {

        [Required(ErrorMessage = "The field with name {0} is required.")]
        public string LocationNumber { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        public int Quantity { get; set; }
        
        public DateTime LastMovementDate { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        public string LocationLevel { get; set; }
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public Guid MasterDataId { get; set; }
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public Guid StockTypeId { get; set; }
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public Guid LocationId { get; set; }
    }
}