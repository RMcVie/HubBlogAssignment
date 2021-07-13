using System;

namespace HubBlogAssignment.Shared.Read
{
    public class CommentReadDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public string User { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
