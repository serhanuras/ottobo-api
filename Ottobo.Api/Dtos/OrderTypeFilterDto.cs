namespace Ottobo.Api.Dtos
{
    public class OrderTypeFilterDto : IFilterDto
    {
        public long Id { get; set; }
        
        public string TypeName { get; set; }

    }
}
