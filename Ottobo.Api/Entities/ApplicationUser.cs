using System;
using Microsoft.AspNetCore.Identity;

namespace Ottobo.Api.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
