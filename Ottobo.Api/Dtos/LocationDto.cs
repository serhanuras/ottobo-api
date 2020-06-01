using System;

namespace Ottobo.Api.Dtos
{
    public class LocationDto: IDto
    {
        public Guid Id { get; set; }

        public long MapId { get; set; }
        
        public string Name { get; set; }
        
        public string XCoordinate { get; set; }
        
        public string YCoordinate { get; set; }
        
        public string Theate { get; set; }
        
    }
}