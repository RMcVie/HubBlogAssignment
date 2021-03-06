using System;

namespace HubBlogAssignment.Shared.Read
{
    public class PostReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
