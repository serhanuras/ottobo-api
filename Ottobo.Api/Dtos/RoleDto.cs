using System;
using Newtonsoft.Json;

namespace Ottobo.Api.Dtos
{
    public class RoleDto:IDto
    {
       
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}