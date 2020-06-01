using System;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class OrderTypeDto : IDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

    }
}
