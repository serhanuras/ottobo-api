namespace Ottobo.Api.Dtos
{
    public class RoleFilterDto:IFilterDto
    {
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; }
    }
}