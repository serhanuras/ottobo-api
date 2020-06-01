using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class OrderPatchDto : IPatchDto
    {

    
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int TownId { get; set; }
        
        public Guid OrderTypeId { get; set; }

    }
}
