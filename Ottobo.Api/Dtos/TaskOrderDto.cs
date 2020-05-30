namespace Ottobo.Api.Dtos
{
    public class TaskOrderDto:IDto
    {
        public long Id { get; set; }
        
        public OrderDto Order { get; set; }
        
        public LocationDto Location { get; set; }
        
        public RobotTaskDto RobotTask { get; set; }
        
        public long RobotTaskId { get; set; }
    }
}