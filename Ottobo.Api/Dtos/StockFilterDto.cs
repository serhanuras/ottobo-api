namespace Ottobo.Api.Dtos
{
    public class StockFilterDto : IFilterDto
    {
        
        public long LocationId { get; set; }

        public string LocationNumber { get; set; }

        public int StockTypeId { get; set; }

        public string OrderingField { get; set; }
        
        public bool AscendingOrder { get; set; } = true;

    }
}
