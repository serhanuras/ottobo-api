using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class RobotTaskCreationDto:ICreationDto
    {
        [Required(ErrorMessage = "The field with name {0} is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        public Guid RobotId { get; set; }
        
    }
}