using System;

namespace HubBlogAssignment.Shared
{
    public class CommentDb
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}