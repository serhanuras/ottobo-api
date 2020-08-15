namespace Ottobo.Api.Dtos
{
    public class TaskOrderFilterDto:IFilterDto
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; }
    }
}