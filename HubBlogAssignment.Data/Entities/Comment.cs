using HubBlogAssignment.Data.Entities.Database;

namespace HubBlogAssignment.Data.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public User User { get; set; }
        public int VotesCount { get; set; }
    }
}
