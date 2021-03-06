using System;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos
{
    public class UserInfoDto : IDto
    {
        public Guid Id { get; set; }
        
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        
    }
}