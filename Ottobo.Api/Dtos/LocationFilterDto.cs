namespace Ottobo.Api.Dtos
{
    public class LocationFilterDto: IFilterDto
    {
        public long Id { get; set; }

        public long MapId { get; set; }
        
        public string XCoordinate { get; set; }
        
        public string YCoordinate { get; set; }
        
        public string Theate { get; set; }
        
    }
}