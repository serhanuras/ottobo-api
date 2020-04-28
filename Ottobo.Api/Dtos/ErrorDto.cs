using System.Runtime.Serialization;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    [DataContract]
    public class ErrorDto
    {
        public ErrorDto()
        {
            
        }

        public ErrorDto(string message)
        {
            this.Message = message;

        }
        
        [DataMember(EmitDefaultValue = false)]
        public string Message { get; set; }
    }
}