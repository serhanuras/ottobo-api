namespace Ottobo.Api.Dtos
{
    public class OrderTypeDto : IDto
    {
        public long Id { get; set; }
        
        public string TypeName { get; set; }

    }
}
