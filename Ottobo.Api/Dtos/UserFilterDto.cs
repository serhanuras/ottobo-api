namespace Ottobo.Api.Dtos
{
    public class UserFilterDto:IFilterDto
    {
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; }
        
        
        public string Email { get; set; }


        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}