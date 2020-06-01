using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class OrderCreationDto: ICreationDto
    {
        
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        public int TownId { get; set; }
        
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public Guid OrderTypeId { get; set; }

    }
}
