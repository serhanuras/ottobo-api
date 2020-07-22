using System;

namespace Ottobo.Entities
{
    public class ApiLog:IEntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        
        public DateTime LastAccessed { get; set; }
        
        public string MethodName { get; set; }
        
        public string ControllerName { get; set; }
        
        public string LogType { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public string ActionBy { get; set; }
        
        public string RequestBody { get; set; }
        
        public string ResponseBody { get; set; }
        
        public string Exception { get; set; }
        
        
    }
}