using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class Robot: EntityBase
    {

        public string Name { get; set; }
        
        public ICollection<RobotTask> RobotTaskList { get; set; }
        
    }
}