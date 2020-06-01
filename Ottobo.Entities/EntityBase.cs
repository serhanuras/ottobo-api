using System;

namespace Ottobo.Entities
{
    public class EntityBase:IEntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime LastAccessed { get; set; }
    }
}