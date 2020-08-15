using System;
using Microsoft.AspNetCore.Identity;

namespace Ottobo.Entities
{
    public class User : IEntityBase
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Password { get; set; }
        
        public Role Role { get; set; }
        
        public Guid RoleId { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public DateTime? UpdatedOn { get; set; }

        public DateTime LastAccessed { get; set; }
        public DateTime BirthDate { get; set; }

        
       
        
    }
}
