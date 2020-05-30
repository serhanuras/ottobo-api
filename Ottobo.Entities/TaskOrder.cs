using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class TaskOrder: IEntity
    {
        [Key]
        public long Id { get; set; }
        
        public Order Order { get; set; }
        
        public Location Location { get; set; }
        
        public RobotTask RobotTask { get; set; }
        
        public long RobotTaskId { get; set; }
    }
}