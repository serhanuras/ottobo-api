namespace Ottobo.Api.Dtos
{
    public class LocationFilterDto: IFilterDto
    {

        public long MapId { get; set; }
        
        public string XCoordinate { get; set; }
        
        public string YCoordinate { get; set; }
        
        public string Theate { get; set; }

        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; }
    }
}