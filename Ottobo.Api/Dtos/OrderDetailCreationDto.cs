using System;
using System.ComponentModel.DataAnnotations;
using Ottobo.Api.Attributes;

namespace Ottobo.Api.Dtos
{
    public class OrderDetailCreationDto : ICreationDto
    {
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public int Quantity { get; set; }
        
        [NumberInRange(0,3)]
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public int BasketId { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        public int PickedQuantity { get; set; }
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public Guid StockId { get; set; }
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public Guid OrderId { get; set;}
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public Guid RobotTaskId { get; set; }

    }
}
