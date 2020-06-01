using System;

namespace Ottobo.Api.Dtos
{
    public class OrderFilterDto : IFilterDto
    {
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int TownId { get; set; }
        
        public Guid OrderTypeId { get; set; }


        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;

    }
}
