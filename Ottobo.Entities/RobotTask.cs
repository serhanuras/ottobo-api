using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class RobotTask: IEntity
    {
        [Key]
        public long Id { get; set; }
        
        public Robot Robot { get; set; }

        public ICollection<TaskOrder> TaskOrders { get; set; }
    }
}