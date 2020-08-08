namespace Ottobo.Api.Dtos
{
    public class RobotTaskFilterDto:IFilterDto
    {
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; }
    }
}