using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class UserCreationDto:ICreationDto
    {
        public string Id { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        
        public string CurrentPassword { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string RoleId { get; set; }
    }
}