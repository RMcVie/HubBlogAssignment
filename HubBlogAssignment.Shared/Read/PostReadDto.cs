using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Shared
{
    public class PostReadDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
