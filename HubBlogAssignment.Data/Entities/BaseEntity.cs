using System;

namespace HubBlogAssignment.Data.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDateTimeUtc = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
