using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
