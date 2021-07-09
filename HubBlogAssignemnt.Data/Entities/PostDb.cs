using System;
using System.Collections.Generic;

namespace HubBlogAssignment.Shared
{
    public class PostDb
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public ICollection<CommentDb> Comments { get; set; }
    }
}
