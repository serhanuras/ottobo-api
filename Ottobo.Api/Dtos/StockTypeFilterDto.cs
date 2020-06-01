using System;

namespace Ottobo.Api.Dtos
{
    public class StockTypeFilterDto : IFilterDto
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

    }
}
