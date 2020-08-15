using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Ottobo.Entities
{
    public class Role : IEntityBase
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public List<User> Users { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime? UpdatedOn { get; set; }
        
        public DateTime LastAccessed { get; set; }
    }
}