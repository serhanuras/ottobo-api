using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class RobotTask: EntityBase
    {

        public string Name { get; set; }
        
        public Robot Robot { get; set; }
        
        public Guid RobotId { get; set; }
        
        public List<OrderDetail> OrderDetailList { get; set; }
        

    }
}