namespace Ottobo.Api.Dtos
{
    public class TaskOrderCreationDto:ICreationDto
    {
        public long Id { get; set; }
        
        public long OrderId { get; set; }
        
        public long LocationId { get; set; }

        public long RobotTaskId { get; set; }
    }
}