using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ottobo.Api.Dtos
{
    public class EditRoleDto
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}