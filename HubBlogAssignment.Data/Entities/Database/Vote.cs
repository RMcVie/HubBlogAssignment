using HubBlogAssignment.Shared;

namespace HubBlogAssignment.Data.Entities
{
    public class Vote : BaseEntity
    {
        public User User { get; set; }
        public PostDb Post { get; set; }
        public CommentDb Comment { get; set; }
    }
}
