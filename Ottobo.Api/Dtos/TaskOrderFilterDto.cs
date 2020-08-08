namespace Ottobo.Api.Dtos
{
    public class TaskOrderFilterDto:IFilterDto
    {
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; }
    }
}